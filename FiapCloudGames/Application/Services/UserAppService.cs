using Application.DTOs.Results;
using Application.DTOs.Signatures;
using Application.Interfaces;
using Core.Entity;
using Core.Enums;
using Core.Interfaces.Repository;

namespace Application.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserRepository _userRepository;
        public UserAppService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> CreateUser(CreateUserSignature signature)
        {
            var user = new User
            {
                UserName = signature.UserName,
                Email = signature.Email,
                Password = signature.Password,
                UserType = UserType.CommonUser
            };

            var idUser = _userRepository.Create(user);

            return true;
        }

        public Task<bool> DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UsersResult>> GetUsers(UsersSignature signature)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUser(UsersSignature signature)
        {
            throw new NotImplementedException();
        }
    }
}
