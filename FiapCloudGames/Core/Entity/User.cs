using Core.Entity;
using Core.Entity.Base;
using Core.Enums;
using Core.ValueObjects;
using Microsoft.AspNetCore.Identity;

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
        var result = GetPasswordVerificationResult(plainTextPassword);
        return !result.Equals(PasswordVerificationResult.Failed);
    }

    public void ChangePassword(string currentPassword, string newPassword)
    {
        if (!GetPasswordVerificationResult(currentPassword).Equals(PasswordVerificationResult.Success))
            throw new InvalidOperationException("Senha inválida.");

        if (GetPasswordVerificationResult(newPassword).Equals(PasswordVerificationResult.Success))
            throw new InvalidOperationException("A nova senha não pode ser igual a senha atual.");

        UpdatePassword(new Password(newPassword));
    }

    public void ForceChangePassword(Password password)
    {
        UpdatePassword(password);
    }

    public PasswordVerificationResult GetPasswordVerificationResult(string plainTextPassword)
        => Password.Verify(plainTextPassword);

    private void UpdatePassword(Password password)
    {
        Password = password;
    }
}
