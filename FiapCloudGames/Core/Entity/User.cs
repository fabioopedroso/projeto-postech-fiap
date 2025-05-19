using Core.Entity;
using Core.Entity.Base;
using Core.Enums;
using Core.ValueObjects;

public class User : EntityBase
{
    public string UserName { get; private set; }
    public Email Email { get; private set; }
    public Password Password { get; private set; }
    public UserType UserType { get; private set; }

    public Library Library { get; set; }
    public Cart Cart { get; set; }

    protected User() { }

    public User(string userName, Email email, Password password, UserType userType)
    {
        UserName = userName;
        Email = email;
        Password = password;
        UserType = userType;
    }

    public bool VerifyPassword(string plainTextPassword)
    {
        return Password.Verify(plainTextPassword);
    }
}
