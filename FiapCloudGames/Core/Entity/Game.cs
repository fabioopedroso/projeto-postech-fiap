using Core.Entity.Base;
using Core.ValueObjects;

namespace Core.Entity;
public class Game : EntityBase
{
    public required string Name { get; set; }
    public string Description { get; set; }
    public required string Genre { get; set; }
    public required Price Price { get; set; }

    public ICollection<Library> Libraries { get; set; } = new List<Library>();
    public ICollection<Sale> Sales { get; set; } = new List<Sale>();
    public ICollection<Cart> Carts { get; set; } = new List<Cart>();

    public decimal Amount
        => Price.Amount;

    public decimal GetDiscountPercentage()
    {
        var activeSale = Sales?.FirstOrDefault(s => s.IsCurrentlyActive());
        if (activeSale == null)
            return 0;
        return activeSale.DiscountPercentage.Value;
    }

    public decimal GetDiscountedPrice()
    {
        var activeSale = Sales?.FirstOrDefault(s => s.IsCurrentlyActive());
        if (activeSale == null)
            return Price.Amount;

        var discount = activeSale.DiscountPercentage.Value;
        return Math.Round(Price.Amount * (1 - discount), 2);
    }
}
