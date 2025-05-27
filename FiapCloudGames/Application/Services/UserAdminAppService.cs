using Application.DTOs.User.Results;
using Application.DTOs.User.Signatures;
using Application.Exceptions;
using Application.Interfaces;
using Core.Enums;
using Core.Interfaces.Repository;
using Core.ValueObjects;

namespace Application.Services;
public class UserAdminAppService : IUserAdminAppService
{
    private readonly IUserRepository _userRepository;
    private readonly IUserAppService _userAppService;

    public UserAdminAppService(IUserRepository userRepository, IUserAppService userAppService)
    {
        _userRepository = userRepository;
        _userAppService = userAppService;
    }

    public async Task CreateUser(CreateUserDto signature)
    {
        var userType = ParseUserType(signature.Role);

        if (userType == UserType.CommonUser)
        {
            await _userAppService.Register(new RegisterDto
            {
                UserName = signature.UserName,
                Email = signature.Email,
                Password = signature.Password
            });

            return;
        }            

        var email = new Email(signature.Email);
        var password = new Password(signature.Password);

        var userEntity = new User(
            signature.UserName,
            email,
            password,
            userType
        );

        await _userRepository.CreateAsync(userEntity);
    }

    public async Task<IEnumerable<UserDto>> GetUsers()
    {
        var users = await _userRepository.GetAllAsync();

        return users.Select(u => new UserDto
        {
            Id = u.Id,
            UserName = u.UserName,
            Email = u.Email.Address
        });
    }

    public async Task<UserDto> GetUserById(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
            throw new NotFoundException("Usuário não encontrado.");

        return new UserDto
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email.Address
        };
    }

    public async Task PromoteUser(int userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            throw new NotFoundException("Usuário não encontrado.");

        user.SetUserType(UserType.Administrator);

        await _userRepository.UpdateAsync(user);
    }

    public async Task DemoteUser(int userId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
            throw new NotFoundException("Usuário não encontrado.");

        user.SetUserType(UserType.CommonUser);

        await _userRepository.UpdateAsync(user);
    }

    public async Task SetUserActiveStatus(SetUserActiveStatusDto signature)
    {
        var user = await _userRepository.GetByIdAsync(signature.UserId);
        if (user == null)
            throw new NotFoundException("Usuário não encontrado.");

        user.IsActive = signature.IsActive;

        await _userRepository.UpdateAsync(user);
    }

    private UserType ParseUserType(string role)
    {
        return role.ToLower() switch
        {
            "admin" => UserType.Administrator,
            "user" => UserType.CommonUser,
            _ => throw new ArgumentException("Role inválida")
        };
    }
}
