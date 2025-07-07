namespace StockSip.Platform.API.Shared.Domain.Model.ValueObjects;

public record Money(decimal Amount, string Currency)
{
    public override string ToString() => $"{Amount} {Currency}";
}
