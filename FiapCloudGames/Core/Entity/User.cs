using Core.Entity;
using Core.Entity.Base;
using Core.Enums;

public class User : EntityBase
{
    public string UserName { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public UserType UserType { get; private set; }

    public Library Library { get; set; }
    public Cart Cart { get; set; }

    public User(string userName, string email, string password, UserType userType)
    {
        UserName = userName;
        Email = email;
        Password = password;
        UserType = userType;
    }
}
