using Application.DTOs.Auth.Signature;
using Application.Interfaces;
using Core.Interfaces.Repository;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Services;
public class AuthAppService : IAuthAppService
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;

    public AuthAppService(IConfiguration configuration, IUserRepository userRepository)
    {
        _configuration = configuration;
        _userRepository = userRepository;
    }

    public async Task<string> GenerateToken(LoginDto login)
    {
        var user = await GetValidatedUserAsync(login);
        var claims = GenerateClaims(user);
        var token = CreateJwtToken(claims);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    #region PrivateMethods
    private async Task<User> GetValidatedUserAsync(LoginDto login)
    {
        var user = await _userRepository.GetByUserNameAsync(login.UserName);

        if (user is null)
            throw new InvalidOperationException("O usuário não foi encontrado.");

        if (!user.IsActive)
            throw new InvalidOperationException("Usuário informado está inativo.");

        if (!user.VerifyPassword(login.Password))
            throw new InvalidOperationException("Usuário ou senha inválidos.");

        return user;
    }

    private IEnumerable<Claim> GenerateClaims(User user)
    {
        return new[]
        {
            new Claim("UserName", user.UserName),
            new Claim("Email", user.Email.Address),
            new Claim("UserType", user.UserType.ToString())
        };
    }

    private JwtSecurityToken CreateJwtToken(IEnumerable<Claim> claims)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        return new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: signingCredentials
        );
    }
    #endregion
}
