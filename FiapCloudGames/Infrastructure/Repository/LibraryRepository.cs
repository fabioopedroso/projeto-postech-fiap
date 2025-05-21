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

        public async Task AddGames(int userId, IEnumerable<Game> games)
        {
            var library = _context.Set<Library>()
                .Include(l => l.Games)
                .First(l => l.User.Id == userId);

            foreach (var game in games)
                if (!library.Games.Any(g => g.Id == game.Id))
                    library.Games.Add(game);

            await _context.SaveChangesAsync();
        }
    }
}
