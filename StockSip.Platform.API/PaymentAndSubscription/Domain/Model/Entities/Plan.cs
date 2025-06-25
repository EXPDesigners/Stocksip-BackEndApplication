using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.ValueObjects;
using StockSip.Platform.API.Shared.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Entities;

/// This entity represents a subscription plan in the payment and subscription domain.
/// <summary>
/// This class encapsulates the properties and behaviors of a subscription plan, including its unique identifier, plan type, duration, maximum warehouses, maximum products, and price.
/// </summary>
public class Plan
{
    public string PlanId { get; private set; }
    
    public EPlanType PlanType { get; internal set; }
    
    public string Description { get; private set; }
    
    public EPaymentFrequency EPaymentFrequency { get; internal set; }
    
    public Money Price { get; internal set; }
    
    public int MaxWarehouses { get; private set; }
    
    public int MaxProducts { get; private set; }
    
    /// <summary>
    /// Default constructor for EF Core.
    /// </summary>
    protected Plan() {}
}