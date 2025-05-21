using Application.DTOs.Cart.Shared;

namespace Application.DTOs.Cart.Result;
public class CartSummaryDto
{
    public int ItemCount { get; set; } = 0;
    public decimal TotalPrice { get; set; } = 0.0m;
    public List<CartGameData> Games { get; set; } = new List<CartGameData>();
}
