using System.Text.Json.Serialization;

namespace Application.DTOs.Game.Signature;
public class SetActiveStatusDto
{
    public int Id { get; set; }
    public bool IsActive { get; set; }
}
