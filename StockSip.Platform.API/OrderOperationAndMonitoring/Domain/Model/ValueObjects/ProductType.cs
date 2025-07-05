namespace StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.ValueObjects;

public record ProductType
{
    public string Value { get; }

    public ProductType(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("ProductType cannot be empty.", nameof(value));
        if (value.Length > 50)
            throw new ArgumentException("ProductType max length is 50 characters.", nameof(value));

        Value = value.Trim();
    }
    
    public static implicit operator string(ProductType p) => p.Value;
    public static implicit operator ProductType(string v) => new(v);

    public override string ToString() => Value;
}