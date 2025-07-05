namespace StockSip.Platform.API.PaymentAndSubscription.Interfaces.REST.Resources;

/// <summary>
/// This record represents an account resource in the Payment and Subscription API.
/// </summary>
public record AccountResource(
    string AccountId, 
    string Email,
    string BusinessName, 
    string Status,
    string AccountRole,
    string StreetAddress,
    string CreatedTime);