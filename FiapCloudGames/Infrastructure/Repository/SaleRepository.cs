using Core.Entity;
using Core.Interfaces.Repository;
using Infrastructure.Persistense;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository;
public class SaleRepository : Repository<Sale>, ISaleRepository
{
    public SaleRepository(ApplicationDbContext context) : base(context) { }

    public async Task<Sale> GetSaleWithGamesByIdAsync(int id)
    {
        return await _context.Sales
            .Include(s => s.Games)
            .FirstOrDefaultAsync(s => s.Id == id);
    }
}