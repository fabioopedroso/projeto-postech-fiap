using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

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
}
