using Core.Entity;
using Core.Interfaces.Repository;
using Infrastructure.Persistense;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;
public class GameRepository : Repository<Game>, IGameRepository
{
    public GameRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Game> GetGameWithActiveSalesByIdAsync(int gameId)
    {
        return await _context.Games
            .Include(g => g.Sales.Where(s => s.IsActive))
            .FirstOrDefaultAsync(g => g.Id == gameId);
    }

    public async Task<IEnumerable<Game>> GetAllGamesWithSalesAsync()
    {
        return await _context.Games
            .Include(g => g.Sales)
            .ToListAsync();
    }

    public async Task<Game> GetAllGamesWithSalesByIdAsync(int gameId)
    {
        return await _context.Games
            .Include(g => g.Sales)
            .FirstOrDefaultAsync(g => g.Id == gameId);
    }
}
