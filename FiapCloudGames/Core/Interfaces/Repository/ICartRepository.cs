using Core.Entity;

namespace Core.Interfaces.Repository
{
    public interface ICartRepository
    {
        Task CreateAsync(Cart cart);
        Task<Cart> GetCartByUserIdAsync(int userId);
        Task<IEnumerable<Game>> GetGamesByUserIdAsync(int userId);
        Task AddGameAsync(int userId, Game game);
        Task RemoveGameAsync(int userId, Game game);
        Task ClearCartAsync(int userId);
        Task<decimal> GetTotalPriceAsync(int userId);
        Task<int> GetItemCountAsync(int userId);
    }
}
