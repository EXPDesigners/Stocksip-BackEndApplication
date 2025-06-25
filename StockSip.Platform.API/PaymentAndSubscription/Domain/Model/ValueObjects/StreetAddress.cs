namespace StockSip.Platform.API.PaymentAndSubscription.Domain.Model.ValueObjects;

/// <summary>
/// The StreetAddress value object represents a street address for the order bounded context.
/// </summary>
/// <param name="Street">The physical street address.</param>
public record StreetAddress(string Street);