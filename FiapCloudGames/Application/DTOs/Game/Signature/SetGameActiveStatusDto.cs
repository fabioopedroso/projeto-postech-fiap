using System.Text.Json.Serialization;

namespace Application.DTOs.Game.Signature;
public class SetGameActiveStatusDto
{
    public int Id { get; set; }
    public bool IsActive { get; set; }
}
