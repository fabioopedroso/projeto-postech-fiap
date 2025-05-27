using Application.DTOs.User.Results;
using Application.DTOs.User.Signatures;

namespace Application.Interfaces;
public interface IUserAdminAppService
{
    Task CreateUser(CreateUserDto signature);
    Task SetUserActiveStatus(SetUserActiveStatusDto signature);
    Task<IEnumerable<UserDto>> GetUsers();
    Task<UserDto> GetUserById(int id);
    Task PromoteUser(int userId);
    Task DemoteUser(int userId);
}
