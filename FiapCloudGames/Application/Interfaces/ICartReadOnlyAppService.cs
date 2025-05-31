using Application.DTOs.Cart.Result;
using Application.DTOs.Cart.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface ICartReadOnlyAppService
    {
        Task<List<CartGameData>> GetAllGames();
        Task<decimal> GetTotalPrice();
        Task<int> GetItemCount();
        Task<CartSummaryDto> GetCartSummary();
    }
}
