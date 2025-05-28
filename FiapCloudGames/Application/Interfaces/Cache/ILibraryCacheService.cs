using Application.DTOs.Library.Result;
using Core.Entity;

namespace Application.Interfaces.Cache;
public interface ILibraryCacheService
{
    Task<IEnumerable<LibraryGamesDto>> GetCachedLibraryGamesAsync();
    void InvalidateLibraryGamesCache(int userId);
}
