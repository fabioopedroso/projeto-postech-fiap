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
        private readonly ICurrentUserAppService _currentUser;

        public UserAppService(IUnitOfWork unitOfWork, ICurrentUserAppService currentUserAppService)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUserAppService;
        }

        public async Task Register(RegisterDto signature)
        {
            await EnsureEmailIsUnique(signature.Email);
            await EnsureUserNameIsUnique(signature.UserName);

            var user = BuildUser(signature);

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

        public Task DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<UserDto>> GetUsers(UserSignature signature)
        {
            throw new NotImplementedException();
        }

        public Task UpdateUser(UserSignature signature)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<GameDto>> ListLibraryGamesAsync()
        {
            var user = await _unitOfWork.Users.GetUserLibraryGamesAsync(_currentUser.UserId);

            if (user?.Library?.Games == null)
                return Enumerable.Empty<GameDto>();

            return user.Library.Games.Select(game => new GameDto
            {
                Id = game.Id,
                CreationDate = game.CreationDate,
                IsActive = game.IsActive,
                Name = game.Name,
                Description = game.Description,
                Genre = game.Genre,
                Price = game.Price
            });
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
