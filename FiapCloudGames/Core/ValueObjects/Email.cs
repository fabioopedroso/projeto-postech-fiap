using System.Net.Mail;

namespace Core.ValueObjects;
public class Email
{
    public string Address{ get; private set; }

    public Email(string address)
    {
        if (!IsValidEmail(address))
            throw new ArgumentException("O e-mail informado é inválido.", nameof(address));
        Address = address;
    }

    public Email()
    {
        Address = string.Empty;
    }

    public static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return false;

        try
        {
            var mailAddress = new MailAddress(email);
            return mailAddress.Address == email;
        }
        catch
        {
            return false;
        }
    }

    public override string ToString() => Address;
}
