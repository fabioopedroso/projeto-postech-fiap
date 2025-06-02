using System.ComponentModel.DataAnnotations;

namespace Core.ValueObjects;
public class Price
{
    public decimal Amount { get; }

    public Price(decimal amount)
    {
        if (amount < 0)
            throw new ValidationException("O preço não pode ser negativo.");

        Amount = amount;
    }
}
