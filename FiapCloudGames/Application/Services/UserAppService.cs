using Application.Contracts;
using Application.DTOs.Game.Result;
using Application.DTOs.User.Results;
using Application.DTOs.User.Signatures;
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
        private readonly ICurrentUseService _currentUser;

        public UserAppService(IUnitOfWork unitOfWork, ICurrentUseService currentUserAppService)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUserAppService;
        }

        public async Task Register(RegisterDto dto)
        {
            await EnsureEmailIsUnique(dto.Email);
            await EnsureUserNameIsUnique(dto.UserName);

            var user = BuildUser(dto);

            await _unitOfWork.BeginTransactionAsync();

            try
            {
                await PersistUserAsync(user);
                await _unitOfWork.CommitAsync();
            }
            catch (Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw new ApplicationException("Ocorre um erro ao registrar o usuário.", ex);
            }
        }

        public async Task ChangePassword(ChangePasswordDto dto)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(_currentUser.UserId);
            if (user is null)
                throw new InvalidOperationException("Usuário não encontrado.");

            user.ChangePassword(dto.Password, dto.NewPassword);

            await _unitOfWork.Users.UpdateAsync(user);
        }

        public async Task DeleteUser(DeleteUserDto dto)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(_currentUser.UserId);
            if (user is null)
                throw new InvalidOperationException("Usuário não encontrado.");

            if (!user.VerifyPassword(dto.Password))
                throw new InvalidOperationException("Senha inválida.");

            await _unitOfWork.Users.DeleteAsync(user);
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

        private User BuildUser(RegisterDto signature)
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
