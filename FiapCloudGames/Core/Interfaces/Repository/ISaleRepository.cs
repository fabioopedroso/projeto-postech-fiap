using Core.Entity;

namespace Core.Interfaces.Repository;
public interface ISaleRepository : IRepository<Sale>
{
    Task<Sale> GetSaleWithGamesByIdAsync(int id);
}
