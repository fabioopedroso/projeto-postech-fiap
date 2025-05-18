using Core.Entity;
using Core.ValueObjects;

namespace Core.Interfaces.Repository;
public interface IUserRepository : IRepository<User>
{
    bool ExistsByEmail(Email email);
}
