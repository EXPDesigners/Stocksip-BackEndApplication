using StockSip.Platform.API.payment_and_subscription.domain.ValueObjects;

namespace StockSip.Platform.API.payment_and_subscription.domain.aggregates;

/// This aggregate represents an account in the payment and subscription domain.
/// <summary>
/// This class encapsulates the properties and behaviors of an account, including its unique identifier and associated email.
/// </summary>
public class Account
{
    /// <summary>
    /// The account id represents the unique identifier of the account
    /// </summary>
    public string AccountId { get; private set; } = Guid.NewGuid().ToString();
    
    /// <summary>
    /// This email represents the general email that associated all the accounts 
    /// </summary>
    public GeneralEmail Email { get; private set; }
    
    public Role Role { get; private set; }
    
    public Plan PlanType { get; private set; }
    
    
}