using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.ValueObjects;
using StockSip.Platform.API.Shared.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.payment_and_subscription.Domain.Model.Entities;

/// This entity represents a subscription plan in the payment and subscription domain.
/// <summary>
/// This class encapsulates the properties and behaviors of a subscription plan, including its unique identifier, plan type, duration, maximum warehouses, maximum products, and price.
/// </summary>
public class SubscriptionPlan
{
    public string SubscriptionId { get; private set; }
    
    public PlanType PlanType { get; private set; }
    
    public TimeSpan Duration { get; private set; }
    
    public int MaxWarehouses { get; private set; }
    
    public int MaxProducts { get; private set; }
    
    public Money Price { get; internal set; }
    
    /// <summary>
    /// Default constructor for EF Core.
    /// </summary>
    public SubscriptionPlan() {}

    public SubscriptionPlan (string name, TimeSpan duration, int maxWarehouses, int maxProducts, Money price)
    {
        SubscriptionId = Guid.NewGuid().ToString();
        PlanType = new PlanType(name);
        Duration = duration;
        MaxWarehouses = maxWarehouses;
        MaxProducts = maxProducts;
        Price = price;
    }
}