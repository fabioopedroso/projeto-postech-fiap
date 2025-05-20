using Application.DTOs.User.Results;
using Application.DTOs.User.Signatures;

namespace Application.Interfaces
{
    public interface IUserAppService
    {
        Task Register(RegisterDto signature);
        Task UpdateUser(UserSignature signature);
        Task DeleteUser(int id); //soft delete
        Task<IEnumerable<UserDto>> GetUsers(UserSignature signature);
    }
}
