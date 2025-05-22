namespace Application.DTOs.User.Signatures;
public class SetUserActiveStatusDto
{
    public int UserId { get; set; }
    public bool IsActive { get; set; }
}