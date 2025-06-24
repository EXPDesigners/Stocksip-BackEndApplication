namespace StockSip.Platform.API.Shared.Domain.Model.ValueObjects;

public record Currency(string Code)
{
    /// <summary>
    /// Gets the ISO 4217 currency code.
    /// </summary>
    public string Code { get; } = Code;

    public override string ToString() => Code;
    

    public override int GetHashCode() => Code.GetHashCode();
}