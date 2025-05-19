using Application.DTOs.Results;
using Application.DTOs.Signatures;
using Application.Interfaces;
using Core.Entity;
using Core.Enums;
using Core.Interfaces.Repository;
using Core.ValueObjects;

namespace Application.Services
{
    public class UserAppService : IUserAppService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserAppService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateUser(CreateUserSignature signature)
        {
            var email = new Email(signature.Email);

            if (await _unitOfWork.Users.ExistsByEmailAsync(email))
                throw new InvalidOperationException("E-mail já cadastrado.");

            var password = new Password(signature.Password);

            var user = new User(signature.UserName, email, password, UserType.CommonUser);

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                await _unitOfWork.Users.CreateAsync(user);

                await _unitOfWork.Libraries.CreateAsync(new Library { UserId = user.Id });
                await _unitOfWork.Carts.CreateAsync(new Cart { UserId = user.Id });
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
