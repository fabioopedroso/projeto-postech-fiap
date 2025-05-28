using Application.Contracts;
using Application.DTOs.Library.Result;
using Application.Interfaces;
using Application.Interfaces.Cache;
using Core.Entity;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Services.Cache;
public class LibraryCacheService : ILibraryCacheService
{
    private readonly IMemoryCache _memoryCache;
    private readonly ILibraryAppService _libraryAppService;
    private readonly ICurrentUseService _currentUserService;

    public LibraryCacheService(IMemoryCache memoryCache, ILibraryAppService libraryAppService, ICurrentUseService currentUserService)
    {
        _memoryCache = memoryCache;
        _libraryAppService = libraryAppService;
        _currentUserService = currentUserService;
    }

    public async Task<IEnumerable<LibraryGamesDto>> GetCachedLibraryGamesAsync()
    {
        var userId = _currentUserService.UserId;
        var cacheKey = $"LibraryGamesCache_{userId}";

        var result = await _memoryCache.GetOrCreateAsync(cacheKey, async entry =>
        {
            entry.SlidingExpiration = TimeSpan.FromMinutes(10);
            return await _libraryAppService.ListLibraryGamesAsync() ?? Enumerable.Empty<LibraryGamesDto>();
        });

        return result ?? Enumerable.Empty<LibraryGamesDto>();
    }

    public void InvalidateLibraryGamesCache(int userId)
    {
        var cacheKey = $"LibraryGamesCache_{userId}";
        _memoryCache.Remove(cacheKey);
    }
}