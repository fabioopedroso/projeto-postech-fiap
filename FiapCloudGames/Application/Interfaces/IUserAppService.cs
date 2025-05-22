using Application.DTOs.Game.Result;
using Application.DTOs.User.Results;
using Application.DTOs.User.Signatures;

namespace Application.Interfaces
{
    public interface IUserAppService
    {
        Task Register(RegisterDto dto);
        Task ChangePassword(ChangePasswordDto dto);
        Task DeleteUser(DeleteUserDto dto);
    }
}
