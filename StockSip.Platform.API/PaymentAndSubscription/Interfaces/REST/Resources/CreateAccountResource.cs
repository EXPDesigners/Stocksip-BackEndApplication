namespace StockSip.Platform.API.PaymentAndSubscription.Interfaces.Rest.Resources;

public record CreateAccountResource(
    string UserId,
    string Email,
    string Role,
    string BusinessName,
    string StreetAddress);