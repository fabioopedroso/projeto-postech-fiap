namespace Core.Entity;

public class Sale : EntityBase
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal DiscountPercentage { get; set; }

    public ICollection<Game> Games { get; set; }
}
