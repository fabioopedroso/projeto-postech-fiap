using Core.Entity.Base;

namespace Core.Entity;
public class Game : EntityBase
{
    public required string Name { get; set; }
    public string Description { get; set; }
    public required string Genre { get; set; }
    public required decimal Price { get; set; }

    public ICollection<Library> Libraries { get; set; } = new List<Library>();
    public ICollection<Sale> Sales { get; set; } = new List<Sale>();
    public ICollection<Cart> Carts { get; set; } = new List<Cart>();
}
