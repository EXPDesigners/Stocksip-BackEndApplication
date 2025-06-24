using StockSip.Platform.API.payment_and_subscription.Domain.Model.Entities;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Aggregates;

namespace StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Entities;

/// This entity represents a subscription in the payment and subscription domain.
/// <summary>
/// This class encapsulates the properties and behaviors of a subscription, including its unique identifier, associated account, subscription plan, creation date, expiration date, and active status.
/// </summary>
public class Subscription
{
    public string SubscriptionId { get; private set; } = Guid.NewGuid().ToString();
    
    public Account Account { get; internal set; }
    public string AccountId { get; private set; }
    public SubscriptionPlan SubscriptionPlan { get; internal set; }
    
    public string SubscriptionPlanId { get; private set; }
    
    public DateTime CreatedDate { get; internal set; }
    public DateTime ExpiredDate { get; internal set; }
    public bool IsActive => ExpiredDate > DateTime.UtcNow;
    
    /// <summary>
    /// Default constructor for EF Core.
    /// </summary>
    public Subscription() {}
    
    public Subscription(string accountId, SubscriptionPlan subscriptionPlan, DateTime expiredDate)
    {
        AccountId = accountId;
        SubscriptionPlan = subscriptionPlan;
        ExpiredDate = expiredDate;
    }
}