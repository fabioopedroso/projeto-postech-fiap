namespace Core.ValueObjects;
public class Price
{
    public decimal Amount { get; }

    public Price(decimal amount)
    {
        if (amount < 0)
            throw new ArgumentException("O preço não pode ser negativo.", nameof(amount));

        Amount = amount;
    }
}
