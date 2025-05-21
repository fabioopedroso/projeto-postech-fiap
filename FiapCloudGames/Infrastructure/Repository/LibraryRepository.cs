using Core.Entity;
using Core.Interfaces.Repository;
using Infrastructure.Persistense;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class LibraryRepository : ILibraryRepository
    {
        protected ApplicationDbContext _context;
        protected DbSet<Library> _dbSet;

        public LibraryRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Library>();
        }

        public async Task CreateAsync(Library library)
        {
            _dbSet.Add(library);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Game>> GetGamesByUserIdAsync(int userId)
        {
            return await _context.Set<Library>()
                .Where(l => l.User.Id == userId)
                .SelectMany(l => l.Games)
                .ToListAsync();
        }
    }
}
