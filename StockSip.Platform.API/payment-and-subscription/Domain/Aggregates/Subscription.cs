using StockSip.Platform.API.payment_and_subscription.domain.ValueObjects;

namespace StockSip.Platform.API.payment_and_subscription.domain.aggregates;

/// This aggregate represents a subscription in the payment and subscription domain.
/// <summary>
/// This class encapsulates the properties and behaviors of a subscription, including its unique identifier, plan ID, account ID, start date, end date, and status.
/// </summary>
public class Subscription
{
    public string SubscriptionId { get; private set; } = Guid.NewGuid().ToString();
    
    public string PlanId { get; set; }
    
    public string AccountId { get; set; }
    
    public DateTime StartDate { get; internal set; }
    
    public DateTime? EndDate { get; internal set; }

    public EStatus Status { get; set; } = EStatus.ACTIVE;
    
    /// <summary>
    /// Default constructor for EF Core.
    /// </summary>
    private Subscription() {}
}