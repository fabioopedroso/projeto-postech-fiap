using Application.Contracts;
using Application.DTOs.Cart.Result;
using Application.Interfaces;
using Application.Interfaces.Cache;
using Microsoft.Extensions.Caching.Memory;

namespace Application.Services.Cache;
public class CartCacheService : ICartCacheService
{
    private readonly IMemoryCache _memoryCache;
    private readonly ICartAppService _cartAppService;
    private readonly ICurrentUseService _currentUserService;

    public CartCacheService(IMemoryCache memoryCache, ICartAppService cartAppService, ICurrentUseService currentUserService)
    {
        _memoryCache = memoryCache;
        _cartAppService = cartAppService;
        _currentUserService = currentUserService;
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
