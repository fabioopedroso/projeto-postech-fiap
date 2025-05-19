using Core.Entity;

namespace Core.Interfaces.Repository
{
    public interface ICartRepository
    {
        Task CreateAsync(Cart cart);
    }
}
