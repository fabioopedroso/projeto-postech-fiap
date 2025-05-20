using Application.DTOs.Auth.Signature;

namespace Application.Interfaces;
public interface IAuthAppService
{
    Task<string> GenerateToken(LoginDto user);
}
