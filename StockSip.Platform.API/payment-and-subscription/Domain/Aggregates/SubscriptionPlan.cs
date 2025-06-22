using StockSip.Platform.API.payment_and_subscription.domain.ValueObjects;
using StockSip.Platform.API.Shared.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.payment_and_subscription.domain.aggregates;

/// This aggregate represents a subscription plan in the payment and subscription domain.
/// <summary>
/// This class encapsulates the properties and behaviors of a subscription plan, including its unique identifier, name, plan type, limits, price, and whether it is recurring.
/// </summary>
public class SubscriptionPlan
{
    public string SubscriptionId { get; private set; } = Guid.NewGuid().ToString();
    
    public string Name { get; private set; }
    
    public PlanType PlanType { get; internal set; }
    
    public PlanLimits PlanLimits { get; internal set; }
    
    public Money Price { get; internal set; }
    
    public bool IsRecurring { get; internal set; }
    
    /// <summary>
    /// Default constructor for EF Core.
    /// </summary>
    public SubscriptionPlan() {}
}