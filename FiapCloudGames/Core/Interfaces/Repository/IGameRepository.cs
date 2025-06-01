using Core.Entity;

namespace Core.Interfaces.Repository;
public interface IGameRepository : IRepository<Game>
{
    Task<Game> GetGameWithActiveSalesByIdAsync(int gameId);
    Task<IEnumerable<Game>> GetAllGamesWithSalesAsync();
    Task<Game> GetAllGamesWithSalesByIdAsync(int gameId);

}
