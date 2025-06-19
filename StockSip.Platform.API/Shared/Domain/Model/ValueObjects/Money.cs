namespace StockSip.Platform.API.Shared.Domain.Model.ValueObjects;

public record Money(double Amount, string Currency)
{
    public override string ToString() => $"{Amount} {Currency}";
}
