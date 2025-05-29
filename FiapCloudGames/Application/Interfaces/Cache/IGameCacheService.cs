using Application.DTOs.Game.Result;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Cache;
public interface IGameCacheService
{
    Task<IEnumerable<GameDto>> GetCachedGamesAsync();
    Task<GameDto?> GetCachedGameByIdAsync(int id);
    void InvalidateGamesCache();
    void InvalidateGameCache(int id);
}
