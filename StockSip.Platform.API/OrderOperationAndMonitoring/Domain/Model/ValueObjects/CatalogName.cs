namespace StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.ValueObjects;

public record CatalogName
{
    public string Value { get; }

    public CatalogName(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Catalog name cannot be empty.", nameof(value));

        if (value.Length > 100)
            throw new ArgumentException("Catalog name must not exceed 100 characters.", nameof(value));

        Value = value;
    }

    public override string ToString() => Value;

    // Implicit conversion for convenience
    public static implicit operator string(CatalogName name) => name.Value;
    public static implicit operator CatalogName(string value) => new CatalogName(value);
}