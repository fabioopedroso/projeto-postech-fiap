namespace Core.ValueObjects;
public class DiscountPercentage
{
    public decimal Value { get; }

    public DiscountPercentage(decimal value)
    {
        if (value <= 0 || value > 1)
            throw new ArgumentOutOfRangeException(nameof(value), "O desconto precisa ser um valor entre 0 e 1.");

        Value = decimal.Round(value, 2);
    }
}
