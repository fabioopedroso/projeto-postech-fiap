using Core.Entity;
using Core.Interfaces.Repository;
using Infrastructure.Persistense;

namespace Infrastructure.Repository;
public class GameRepository : Repository<Game>, IGameRepository
{
    public GameRepository(ApplicationDbContext context) : base(context)
    {
    }
}
