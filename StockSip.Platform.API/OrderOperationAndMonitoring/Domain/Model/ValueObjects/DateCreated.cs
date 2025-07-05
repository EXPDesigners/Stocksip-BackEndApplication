namespace StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.ValueObjects;

public record DateCreated
{
    public DateTime Value { get; }

    public DateCreated(DateTime value)
    {
        if (value == default)
            throw new ArgumentException("DateCreated cannot be default value.", nameof(value));

        Value = value;
    }

    public static DateCreated Now() => new DateCreated(DateTime.UtcNow);

    public override string ToString() => Value.ToString("u");

    // Implicit conversions
    public static implicit operator DateTime(DateCreated date) => date.Value;
    public static implicit operator DateCreated(DateTime value) => new DateCreated(value);
}
