using Core.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Core.Entity;
public class User : EntityBase
{
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public UserType UserType { get; set; }
    
    public ICollection<Game> Games { get; set; }
}
