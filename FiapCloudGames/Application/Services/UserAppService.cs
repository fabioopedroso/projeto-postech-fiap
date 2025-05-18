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
        private readonly IUnitOfWork _unitOfWork;
        private readonly PasswordHasher<User> _passwordHasher;

        public UserAppService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _passwordHasher = new PasswordHasher<User>();
        }

        public async Task<bool> CreateUser(CreateUserSignature signature)
        {
            var email = new Email(signature.Email);

            if (_unitOfWork.Users.ExistsByEmail(email))
                throw new ArgumentException("E-mail já cadastrado.");

            var password = new Password(signature.Password);

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                var user = new User(
                    signature.UserName,
                    email.Address,
                    _passwordHasher.HashPassword(null, password.RawPassword),
                    UserType.CommonUser
                );

                var userId = _unitOfWork.Users.Create(user);

                _unitOfWork.Libraries.Create(new Library { UserId = userId });
                _unitOfWork.Carts.Create(new Cart { UserId = userId });

                await _unitOfWork.CommitAsync();
                return true;
            }
            catch
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
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
