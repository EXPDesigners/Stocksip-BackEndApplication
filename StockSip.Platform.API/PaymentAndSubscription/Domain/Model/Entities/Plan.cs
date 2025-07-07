using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Commands;
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
    
    public EPaymentFrequency PaymentFrequency { get; internal set; }
    
    public Money Price { get; internal set; }
    
    public int MaxWarehouses { get; private set; }
    
    public int MaxProducts { get; private set; }
    
    /// <summary>
    /// Default constructor for EF Core.
    /// </summary>
    protected Plan() {}
    
        private Plan(string planId, EPlanType planType, string description, 
               EPaymentFrequency paymentFrequency, Money price, 
               int maxWarehouses, int maxProducts)
    {
        PlanId = planId;
        PlanType = planType;
        Description = description;
        PaymentFrequency = paymentFrequency;
        Price = price;
        MaxWarehouses = maxWarehouses;
        MaxProducts = maxProducts;
        
        Validate();
    }

    public static Plan CreateFreePlan()
    {
        return new Plan(
            planId: "plan_free",
            planType: EPlanType.Free,
            description: "Free Plan",
            paymentFrequency: EPaymentFrequency.None,
            price: new Money(0m, "USD"),
            maxWarehouses: 4,
            maxProducts: 50
        );
    }

    public static Plan CreatePremiumMonthly()
    {
        return new Plan(
            planId: "plan_premium_monthly",
            planType: EPlanType.PremiumMonthly,
            description: "Monthly Premium Plan",
            paymentFrequency: EPaymentFrequency.Monthly,
            price: new Money(29.99m, "USD"),
            maxWarehouses: 10,
            maxProducts: 100
        );
    }

    public static Plan CreatePremiumAnnual()
    {
        return new Plan(
            planId: "plan_premium_annual",
            planType: EPlanType.PremiumAnnual,
            description: "Annual Premium Plan",
            paymentFrequency: EPaymentFrequency.Annual,
            price: new Money(299.99m, "USD"),
            maxWarehouses: 20,
            maxProducts: 200
        );
    }

    private void Validate()
    {
        if (PlanType == EPlanType.Free && Price.Amount > 0)
            throw new InvalidOperationException("The FREE plan should not have a price greater than 0");

        if (PlanType == EPlanType.Free && PaymentFrequency != EPaymentFrequency.None)
            throw new InvalidOperationException("The FREE plan should have a payment frequency of NONE");

        if (PlanType == EPlanType.PremiumMonthly && Price.Amount <= 0)
            throw new InvalidOperationException("The PREMIUM MONTHLY plan should have a price greater than 0");

        if (PlanType == EPlanType.PremiumMonthly && PaymentFrequency != EPaymentFrequency.Monthly)
            throw new InvalidOperationException("The PREMIUM MONTHLY plan should have a payment frequency of MONTHLY");

        if (PlanType == EPlanType.PremiumAnnual && PaymentFrequency != EPaymentFrequency.Annual)
            throw new InvalidOperationException("The PREMIUM ANNUAL plan should have a payment frequency of ANNUAL");
    }
}