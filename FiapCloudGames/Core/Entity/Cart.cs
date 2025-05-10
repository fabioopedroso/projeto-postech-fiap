namespace Core.Entity;
public class Cart
{
    public required int Id { get; set; }
    public required int GameId { get; set; }

    public ICollection<Game> Games { get; set; }
}