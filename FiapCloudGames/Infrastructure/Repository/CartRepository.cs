using Core.Entity;
using Core.Interfaces.Repository;
using Infrastructure.Persistense;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Infrastructure.Repository
{
    public class CartRepository : ICartRepository
    {
        protected ApplicationDbContext _context;
        protected DbSet<Cart> _dbSet;

        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Cart>();
        }

        public async Task CreateAsync(Cart cart)
        {
            _dbSet.Add(cart);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Game>> GetGamesByUserIdAsync(int userId)
        {
            return await _context.Set<Cart>()
                .Where(c => c.User.Id == userId)
                .SelectMany(c => c.Games)
                .ToListAsync();
        }

        public async Task<Cart> GetCartByUserIdAsync(int userId)
        {
            return await _context.Carts
                .Include(c => c.Games)
                .FirstAsync(c => c.User.Id == userId);
        }

        public async Task AddGameAsync(int userId, Game game)
        {
            var cart = await GetCartByUserIdAsync(userId);

            if (!cart.Games.Contains(game))
            {
                cart.Games.Add(game);
                await _context.SaveChangesAsync();
            }
        }

        public async Task RemoveGameAsync(int userId, Game game)
        {
            var cart = await GetCartByUserIdAsync(userId);

            if (cart.Games.Contains(game))
            {
                cart.Games.Remove(game);
                await _context.SaveChangesAsync();
            }
        }

        public async Task ClearCartAsync(int userId)
        {
            var cart = await GetCartByUserIdAsync(userId);
            cart.Games.Clear();
            await _context.SaveChangesAsync();
        }

        public async Task<decimal> GetTotalPriceAsync(int userId)
        {
            var cart = await GetCartByUserIdAsync(userId);
            return cart.Games.Sum(g => g.Price.Amount);
        }

        public async Task<int> GetItemCountAsync(int userId)
        {
            var cart = await GetCartByUserIdAsync(userId);
            return cart.Games.Count;
        }
    }
}
