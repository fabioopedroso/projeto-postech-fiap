namespace Application.DTOs.Game.Result;
public class GameDto
{
    public int Id { get; set; }
    public DateTime CreationDate { get; set; }
    public bool IsActive { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Genre { get; set; }
    public decimal Price { get; set; }
    public decimal DiscountPercentage { get; set; }
    public decimal DiscountedPrice { get; set; }
}
