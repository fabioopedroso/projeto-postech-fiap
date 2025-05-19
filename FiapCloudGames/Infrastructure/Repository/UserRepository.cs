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
        {
            return _dbSet.AnyAsync(user => user.Email == email);
        }

        public async Task<User?> GetByEmailAsync(Email email)
            => await _dbSet.FirstOrDefaultAsync(u => u.Email.Address == email.Address);
    }
}
