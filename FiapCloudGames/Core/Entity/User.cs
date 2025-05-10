using Core.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Core.Entity;
public class User : EntityBase
{
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required UserType UserType { get; set; }

    public Library Library { get; set; }
}
