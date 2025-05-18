using Core.Entity;
using Core.Interfaces.Repository;
using Core.ValueObjects;
using Infrastructure.Persistense;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context) { }

        public bool ExistsByEmail(Email email)
        {
            bool exists = _dbSet.Any(u => u.Email == email.Address);
            return exists;
        }
    }
}
