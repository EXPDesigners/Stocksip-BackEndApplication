namespace StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.ValueObjects;

public record BrandName
{
    public string Value { get; }

    public BrandName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Brand name cannot be empty.");
        Value = value;
    }
}
