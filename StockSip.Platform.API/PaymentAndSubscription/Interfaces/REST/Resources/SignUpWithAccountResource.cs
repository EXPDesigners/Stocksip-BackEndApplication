namespace StockSip.Platform.API.PaymentAndSubscription.Interfaces.REST.Resources;

/// <summary>
/// Defines the resource for signing up with an account.
/// </summary>
public record SignUpWithAccountResource(
    string Username,
    string Password,
    string ValidatePassword,
    string AccountRole,
    string BusinessName
);