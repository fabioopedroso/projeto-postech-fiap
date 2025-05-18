using Application.DTOs.Results;
using Application.DTOs.Signatures;
using Application.Interfaces;
using Core.Entity;
using Core.Enums;
using Core.Interfaces.Repository;
using Core.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace Application.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILibraryRepository _libraryRepository;
        private readonly ICartRepository _cartRepository;
        private readonly PasswordHasher<User> _passwordHasher;


        public UserAppService(IUserRepository userRepository, ILibraryRepository libraryRepósitory, ICartRepository cartRepository)
        {
            _userRepository = userRepository;
            _libraryRepository = libraryRepósitory;
            _cartRepository = cartRepository;
            _passwordHasher = new PasswordHasher<User>();

        }

        public async Task<bool> CreateUser(CreateUserSignature signature)
        {
            var email = new Email(signature.Email);

            if (_userRepository.ExistsByEmail(email))
                throw new ArgumentException("E-mail já cadastrado.");

            var rawPassword = new Password(signature.Password);

            var hashedPassword = _passwordHasher.HashPassword(null, rawPassword.RawPassword);

            var user = new User(
                signature.UserName,
                email.Address,
                hashedPassword,
                UserType.CommonUser
            );

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
