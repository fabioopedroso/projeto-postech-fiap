using Core.Entity;
using Core.ValueObjects;

namespace Core.Interfaces.Repository;
public interface IUserRepository : IRepository<User>
{
    Task<bool> ExistsByEmailAsync(Email email);
    Task<User?> GetByEmailAsync(Email email);
    Task<User?> GetByUserNameAsync(string userName);
}
