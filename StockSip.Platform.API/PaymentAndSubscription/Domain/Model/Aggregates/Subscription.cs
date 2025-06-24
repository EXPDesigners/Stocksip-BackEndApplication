using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Entities;

namespace StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Aggregates;

/// This entity represents a subscription in the payment and subscription domain.
/// <summary>
/// This class encapsulates the properties and behaviors of a subscription, including its unique identifier, associated account, subscription plan, creation date, expiration date, and active status.
/// </summary>
public class Subscription
{
    public string SubscriptionId { get; private set; }
    
    public Account Account { get; internal set; }
    public string AccountId { get; private set; }
    public Plan Plan { get; internal set; }
    
    public string PlanId { get; private set; }
    
    public DateTime CreatedDate { get; internal set; }
    public DateTime ExpiredDate { get; internal set; }
    public bool IsActive => ExpiredDate > DateTime.UtcNow;
    
    /// <summary>
    /// Default constructor for EF Core.
    /// </summary>
    public Subscription() {}
    
    public Subscription(string accountId, Plan plan, DateTime expiredDate)
    {
        SubscriptionId = Guid.NewGuid().ToString();
        AccountId = accountId;
        Plan = plan;
        ExpiredDate = expiredDate;
    }
}