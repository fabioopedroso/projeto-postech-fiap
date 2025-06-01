using System.Text.Json.Serialization;

namespace Application.DTOs.Game.Signature;
public class UpdateGameDto
{
    [JsonIgnore]
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string Genre { get; set; }
    public decimal Price { get; set; }
    public string Currency { get; set; }
}
