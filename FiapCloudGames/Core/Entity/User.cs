using Core.Entity.Base;
using Core.Enums;

namespace Core.Entity;
public class User : EntityBase
{
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required UserType UserType { get; set; }

    public Library Library { get; set; }
    public Cart Cart { get; set; }
}
