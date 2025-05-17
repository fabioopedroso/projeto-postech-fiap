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
        private readonly ILibraryRepository _libraryRepository;
        private readonly ICartRepository _cartRepository;

        public UserAppService(IUserRepository userRepository, ILibraryRepository libraryRepósitory, ICartRepository cartRepository)
        {
            _userRepository = userRepository;
            _libraryRepository = libraryRepósitory;
            _cartRepository = cartRepository;
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

            _libraryRepository.Create(new Library { UserId = idUser }); 
            _cartRepository.Create(new Cart { UserId = idUser });   

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
