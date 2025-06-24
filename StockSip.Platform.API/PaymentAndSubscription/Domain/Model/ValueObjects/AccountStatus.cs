namespace StockSip.Platform.API.PaymentAndSubscription.Domain.Model.ValueObjects;

/// <summary>
/// This record represents the status of an account in the payment and subscription domain.
/// </summary>
/// <param name="Status">The status of the account, which can be either "active" or "inactive".</param>
public record AccountStatus(string Status)
{
    public static AccountStatus ACTIVE => new("active");
    public static AccountStatus INACTIVE => new("inactive");
    
    public static bool CanChangeSubscription(AccountStatus status)
    {
        return status.Status == ACTIVE.Status;
    }
    
}