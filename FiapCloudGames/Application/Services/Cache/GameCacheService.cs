using Application.DTOs.Game.Result;
using Application.Interfaces;
using Application.Interfaces.Cache;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services.Cache;
public class GameCacheService : IGameCacheService
{
    private readonly IMemoryCache _memoryCache;
    private readonly IGameReadOnlyAppService _gameAppService;

    private const string GamesCacheKey = "GamesCache";
    private const string GameCacheKeyPrefix = "GameCache_";

    public GameCacheService(IMemoryCache memoryCache, IGameReadOnlyAppService gameAppService)
    {
        _memoryCache = memoryCache;
        _gameAppService = gameAppService;
    }

    public async Task<IEnumerable<GameDto>> GetCachedGamesAsync()
    {
        return await _memoryCache.GetOrCreateAsync(GamesCacheKey, async entry =>
        {
            entry.SlidingExpiration = TimeSpan.FromMinutes(10);
            var games = await _gameAppService.GetAllAsync();
            return games ?? Enumerable.Empty<GameDto>();
        });
    }

    public async Task<GameDto?> GetCachedGameByIdAsync(int id)
    {
        var key = $"{GameCacheKeyPrefix}{id}";
        return await _memoryCache.GetOrCreateAsync(key, async entry =>
        {
            entry.SlidingExpiration = TimeSpan.FromMinutes(10);
            return await _gameAppService.GetByIdAsync(id);
        });
    }

    public void InvalidateGamesCache()
    {
        _memoryCache.Remove(GamesCacheKey);
    }

    public void InvalidateGameCache(int id)
    {
        _memoryCache.Remove($"{GameCacheKeyPrefix}{id}");
    }
}
