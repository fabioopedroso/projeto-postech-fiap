namespace Core.Entity;
public class Cart
{
    public int Id { get; set; }
    public int UserId { get; set; }
    
    public User User { get; set; }
    public ICollection<Game> Games { get; set; } = new List<Game>();
}