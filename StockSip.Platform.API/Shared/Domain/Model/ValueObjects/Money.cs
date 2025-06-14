namespace StockSip.Platform.API.Shared.Domain.Model.ValueObjects;

public record Money(int amount, Currency currency)
{
    /// <summary>
    /// Gets the amount of money.
    /// </summary>
    public int Amount { get; } = amount;

    /// <summary>
    /// Gets the currency of the money.
    /// </summary>
    public Currency Currency { get; } = currency;

    public override string ToString() => $"{Amount} {Currency}";
}
