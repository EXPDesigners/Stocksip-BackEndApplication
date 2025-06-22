namespace StockSip.Platform.API.payment_and_subscription.domain.ValueObjects;

/// <summary>
/// This record represents the status of an account in the payment and subscription domain.
/// </summary>
/// <param name="Status">The status of the account, which can be either "active" or "inactive".</param>
public record AccountStatus(string Status)
{
    public static AccountStatus Active => new("active");
    public static AccountStatus Inactive => new("inactive");
}