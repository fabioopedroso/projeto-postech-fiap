namespace Core.ValueObjects;
public class DateRange
{
    public DateTime Start { get; }
    public DateTime End { get; }

    private DateRange() { }

    public DateRange(DateTime start, DateTime end)
    {
        if (start > end)
            throw new ArgumentException("A data início deve ser anterior a data fim.");

        if (start == end)
            throw new ArgumentException("A data início não pode ser igual a data fim.");

        Start = start;
        End = end;
    }
}

