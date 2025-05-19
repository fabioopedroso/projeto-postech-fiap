using Core.Entity;
using Core.Interfaces.Repository;
using Infrastructure.Persistense;
using Microsoft.EntityFrameworkCore;

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
    }
}
