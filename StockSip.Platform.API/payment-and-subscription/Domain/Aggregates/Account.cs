using StockSip.Platform.API.payment_and_subscription.domain.ValueObjects;

namespace StockSip.Platform.API.payment_and_subscription.domain.aggregates;

/// This aggregate represents an account in the payment and subscription domain.
/// <summary>
/// This class encapsulates the properties and behaviors of an account, including its unique identifier and associated email.
/// </summary>
public class Account 
{
    public string AccountId { get; private set; } = Guid.NewGuid().ToString();
    
    public AccountStatus AccountStatus { get; internal set; } = AccountStatus.Active;
    
    public Role Role { get; internal set; }
    
    public DateTime CreatedDate { get; internal set; } = DateTime.UtcNow;
    
    public UserId UserId { get; internal set; }
    
    public ProfileId ProfileId { get; internal set; }
    
    /// <summary>
    /// Default constructor for EF Core.
    /// </summary>
    private Account() {}


    public Account(UserId userId, ProfileId profileId, Role role)
    {
        UserId = userId;
        ProfileId = profileId;
        Role = role;
    }
    
    
    
}