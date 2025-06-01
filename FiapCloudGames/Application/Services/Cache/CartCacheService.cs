using Application.Contracts;
using Application.DTOs.Cart.Result;
using Application.Interfaces;
using Application.Interfaces.Cache;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Services.Cache;
public class CartCacheService : ICartCacheService
{
    private readonly IMemoryCache _memoryCache;
    private readonly ICurrentUseService _currentUserService;
    private readonly ICartAppService _cartAppService;

    public CartCacheService(IMemoryCache memoryCache, ICurrentUseService currentUserService, ICartAppService cartAppService)
    {
        _memoryCache = memoryCache;
        _currentUserService = currentUserService;
        _cartAppService = cartAppService;
    }

    public async Task<CartSummaryDto> GetCachedCartSummaryAsync()
    {
        var userId = _currentUserService.UserId;
        var cacheKey = $"CartSummary_{userId}";

        return await _memoryCache.GetOrCreateAsync(cacheKey, async entry =>
        {
            entry.SlidingExpiration = TimeSpan.FromMinutes(5);
            return await _cartAppService.GetCartSummary();
        });
    }

    public void InvalidateCartCache(int userId)
    {
        _memoryCache.Remove($"CartSummary_{userId}");
    }
}
