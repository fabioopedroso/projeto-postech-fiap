namespace Core.Entity;
public class Library
{
    public int Id { get; set; }
    public required int UserId { get; set; }

    public User User { get; set; }
    public ICollection<Game> Games { get; set; } = new List<Game>();
}
