using Application.DTOs.User.Results;
using Application.DTOs.User.Signatures;
using Application.Interfaces;
using Core.Interfaces.Repository;

namespace Application.Services;
public class UserAdminAppService : IUserAdminAppService
{
    private readonly IUserRepository userRepository;

    public UserAdminAppService(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task CreateUser(CreateUserDto signature)
    {
        throw new NotImplementedException();
    }

    public async Task AssignRole(RoleDto signature)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<UserDto>> GetUsers()
    {
        throw new NotImplementedException();
    }

    public async Task RemoveRole(RoleDto signature)
    {
        throw new NotImplementedException();
    }

    public async Task SetUserActiveStatus(SetUserActiveStatusDto signature)
    {
        throw new NotImplementedException();
    }
}
