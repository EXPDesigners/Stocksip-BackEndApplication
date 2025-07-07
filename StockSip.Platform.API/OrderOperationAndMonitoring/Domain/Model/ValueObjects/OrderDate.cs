namespace StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.ValueObjects;

public record OrderDate
{
    public DateTime Value { get; init; }
    
    private OrderDate() { }

    public OrderDate(DateTime date)
    {
        if (date == default)
            throw new ArgumentException("Order date must be a valid datetime.", nameof(date));

        Value = date;
    }

    public static implicit operator DateTime(OrderDate d) => d.Value;

    public override string ToString() => Value.ToString("yyyy-MM-dd HH:mm:ss");
}