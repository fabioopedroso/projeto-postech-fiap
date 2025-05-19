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
            await EnsureEmailIsUnique(signature.Email);
            await EnsureUserNameIsUnique(signature.UserName);

            var user = BuildUser(signature);

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                await PersistUserAsync(user);
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

        public Task<IEnumerable<UsersResult>> GetUsers(UserSignature signature)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateUser(UserSignature signature)
        {
            throw new NotImplementedException();
        }

        #region PrivatedMethods
        private async Task EnsureEmailIsUnique(string emailString)
        {
            var email = new Email(emailString);
            if (await _unitOfWork.Users.ExistsByEmailAsync(email))
                throw new InvalidOperationException("O E-mail informado já está cadastrado.");
        }

        private async Task EnsureUserNameIsUnique(string userName)
        {
            if (await _unitOfWork.Users.GetByUserNameAsync(userName) is not null)
                throw new InvalidOperationException("Este nome de usuário já está sendo usado.");
        }

        private User BuildUser(CreateUserSignature signature)
        {
            var email = new Email(signature.Email);
            var password = new Password(signature.Password);
            return new User(signature.UserName, email, password, UserType.CommonUser);
        }

        private async Task PersistUserAsync(User user)
        {
            await _unitOfWork.Users.CreateAsync(user);
            await _unitOfWork.Libraries.CreateAsync(new Library { UserId = user.Id });
            await _unitOfWork.Carts.CreateAsync(new Cart { UserId = user.Id });
        }
        #endregion
    }
}
