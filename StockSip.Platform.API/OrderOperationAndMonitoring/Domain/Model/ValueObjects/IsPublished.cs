namespace StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.ValueObjects;

public record IsPublished(bool Value)
{
    public static implicit operator bool(IsPublished isPublished) => isPublished.Value;

    public static implicit operator IsPublished(bool value) => new(value);

    public override string ToString() => Value.ToString();
}