namespace StockSip.Platform.API.PaymentAndSubscription.Interfaces.Rest.Resources;

public record CreateAccountResource
(
    string OwnerUserId,
    string Email,
    string BusinessName,
    string AccountRole
);