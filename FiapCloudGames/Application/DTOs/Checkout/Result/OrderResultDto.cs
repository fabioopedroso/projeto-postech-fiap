using Application.DTOs.Cart.Shared;

namespace Application.DTOs.Checkout.Result;

public class OrderResultDto
{
    public Guid OrderId { get; set; } = Guid.NewGuid();
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public List<PurchasedGameDto> Games { get; set; }
    public decimal TotalPrice { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
}
