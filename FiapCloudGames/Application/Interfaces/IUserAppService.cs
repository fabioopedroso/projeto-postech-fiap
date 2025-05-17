using Application.DTOs.Results;
using Application.DTOs.Signatures;

namespace Application.Interfaces
{
    public interface IUserAppService
    {
        Task<bool> CreateUser(CreateUserSignature signature);
        Task<bool> UpdateUser(UsersSignature signature);
        Task<bool> DeleteUser(int id); //soft delete
        Task<IEnumerable<UsersResult>> GetUsers(UsersSignature signature);
    }
}
