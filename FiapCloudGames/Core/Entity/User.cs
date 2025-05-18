using Core.Entity;
using Core.Entity.Base;
using Core.Enums;
using Core.ValueObjects;
using Microsoft.AspNetCore.Identity;

public class User : EntityBase
{
    private readonly PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

    public string UserName { get; private set; }
    public string Email { get; private set; }
    public string Password { get; private set; }
    public UserType UserType { get; private set; }

    public Library Library { get; set; }
    public Cart Cart { get; set; }

    protected User() { }

    public User(string userName, string email, Password password, UserType userType)
    {
        UserName = userName;
        Email = email;
        Password = _passwordHasher.HashPassword(this, password.RawPassword);
        UserType = userType;
    }
}
