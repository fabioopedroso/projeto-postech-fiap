using Core.Interfaces.Repository;
using Core.ValueObjects;
using Infrastructure.Persistense;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context) { }

        public Task<bool> ExistsByEmailAsync(Email email)
            => _dbSet.AnyAsync(u => u.Email == email);

        public async Task<User?> GetByEmailAsync(Email email)
            => await _dbSet.FirstOrDefaultAsync(u => u.Email.Address == email.Address);

        public async Task<User?> GetByUserNameAsync(string userName)
            => await _dbSet.FirstOrDefaultAsync(u => u.UserName == userName);

        public async Task<User?> GetUserLibraryGamesAsync(int userId)
            => await _dbSet
                .Include(u => u.Library)
                .ThenInclude(l => l.Games)
                .FirstOrDefaultAsync(u => u.Id == userId);
    }
}
