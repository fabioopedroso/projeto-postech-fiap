using Application.DTOs.Cart.Result;
using Application.DTOs.Cart.Shared;

namespace Application.Interfaces;
public interface ICartAppService
{
    Task AddGame(int gameId);
    Task RemoveGame(int gameId);
    Task<List<CartGameData>> GetAllGames();
    Task ClearCart();
    Task<decimal> GetTotalPrice();
    Task<int> GetItemCount();
    Task<CartSummaryDto> GetCartSummary();
}
