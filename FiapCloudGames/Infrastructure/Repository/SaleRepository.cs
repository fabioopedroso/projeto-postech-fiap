using Core.Entity;
using Core.Interfaces.Repository;
using Infrastructure.Persistense;

namespace Infrastructure.Repository;
public class SaleRepository : Repository<Sale>, ISaleRepository
{
    public SaleRepository(ApplicationDbContext context) : base(context)
    {
    }
}