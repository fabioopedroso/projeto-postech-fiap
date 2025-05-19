using Application.DTOs.Signatures;

namespace Application.Interfaces;
public interface IAuthAppService
{
    Task<string> GenerateToken(LoginDto user);
}
