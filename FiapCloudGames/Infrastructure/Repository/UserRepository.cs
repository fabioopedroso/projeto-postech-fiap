using Core.Entity;
using Core.Interfaces.Repository;
using Infrastructure.Persistense;

namespace Infrastructure.Repository;
public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(ApplicationDbContext context) : base(context)
    {
    }
}
