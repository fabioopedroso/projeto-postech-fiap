namespace Core.ValueObjects;

public class Password
{
    public string RawPassword { get; }

    public Password(string password)
    {
        if (!IsValid(password))
        {
            throw new ArgumentException("A senha deve conter ao menos 8 caracteres, um número, uma letra e um caractere especial.");
        }

        RawPassword = password;
    }

    private static bool IsValid(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            return false;

        if (password.Length < 8)
            return false;

        if (!password.Any(char.IsDigit))
            return false;

        if (!password.Any(char.IsLetter))
            return false;

        if (!password.Any(ch => !char.IsLetterOrDigit(ch)))
            return false;

        return true;
    }

    public override bool Equals(object obj)
    {
        if (obj is Password other)
            return RawPassword == other.RawPassword;
        return false;
    }

    public override int GetHashCode() => RawPassword.GetHashCode();
}
