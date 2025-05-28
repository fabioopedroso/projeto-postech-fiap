using Application.DTOs.Cart.Result;

namespace Application.Interfaces.Cache;
public interface ICartCacheService
{
    Task<CartSummaryDto> GetCachedCartSummaryAsync();
    void InvalidateCartCache(int userId);
}
