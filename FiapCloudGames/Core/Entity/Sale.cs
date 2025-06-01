using Core.Entity.Base;
using Core.ValueObjects;

namespace Core.Entity;

public class Sale : EntityBase
{
    public string Name { get; set; }
    public string Description { get; set; }
    public DateRange Period { get; set; }
    public DiscountPercentage DiscountPercentage { get; set; }

    public ICollection<Game> Games { get; set; } = new List<Game>();

    public DateTime StartDate 
        => Period.Start;
    public DateTime EndDate 
        => Period.End;

    public bool IsCurrentlyActive()
    {
        return IsActive && Period.Start <= DateTime.UtcNow && Period.End >= DateTime.UtcNow;
    }
}
