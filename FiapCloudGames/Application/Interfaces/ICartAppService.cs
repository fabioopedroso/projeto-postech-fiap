using Application.DTOs.Cart.Result;
using Application.DTOs.Cart.Shared;

namespace Application.Interfaces;
public interface ICartAppService
{
    Task AddGame(int gameId);
    Task RemoveGame(int gameId);
    Task ClearCart();
    Task<CartSummaryDto> GetCartSummary();
}
