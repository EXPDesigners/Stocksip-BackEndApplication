using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Entities;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.ValueObjects;

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
    public ESubscriptionStatus SubscriptionStatus { get; private set; }
    
    public DateTime CreatedDate { get; internal set; }
    public DateTime ExpiredDate { get; internal set; }
    
    /// <summary>
    /// Default constructor for EF Core.
    /// </summary>
    protected Subscription() {}
    
    public Subscription(string accountId, Plan plan)
    {
        SubscriptionId = Guid.NewGuid().ToString();
        AccountId = accountId;
        Plan = plan;
        CreatedDate = DateTime.UtcNow;
        ExpiredDate = CalculateExpirationDate(plan);
    }
    
    /// <summary>
    /// This method marks the subscription as active.
    /// </summary>
    public void MarkAsCompleted()
    {
        SubscriptionStatus = ESubscriptionStatus.COMPLETED;
    }
    
    /// <summary>
    /// This method activates the subscription with a new plan, updating the expiration date and status accordingly.
    /// </summary>
    /// <param name="newPlan">The new plan to activate the subscription with.</param>
    /// <exception cref="ArgumentNullException">A null reference was passed for the new plan.</exception>
    public void ActivateWithPlan(Plan newPlan)
    {
        if (newPlan == null)
            throw new ArgumentNullException(nameof(newPlan));

        Plan = newPlan;
        ExpiredDate = CalculateExpirationDate(newPlan);
        SubscriptionStatus = ESubscriptionStatus.COMPLETED;

        if (Account != null && Account.Status == EAccountStatus.INACTIVE)
        {
            Account.ActiveAccount();
        }
    }
    
    /// <summary>
    /// This switch expression calculates the expiration date based on the payment frequency of the plan.
    /// </summary>
    /// <param name="plan">The plan for which the expiration date is to be calculated.</param>
    /// <returns>A DateTime representing the expiration date of the subscription.</returns>
    private DateTime CalculateExpirationDate(Plan plan)
    {
        return plan.PaymentFrequency switch
        {
            EPaymentFrequency.Monthly => DateTime.UtcNow.AddMonths(1),
            EPaymentFrequency.Annual => DateTime.UtcNow.AddYears(1),
            EPaymentFrequency.None => DateTime.MaxValue
        };
    }
    
    public void UpgradePlan(Plan newPlan)
    {
        if (newPlan == null)
            throw new ArgumentNullException(nameof(newPlan));

        if (Plan == null)
            throw new InvalidOperationException("Current subscription has no plan assigned.");

        if (Plan.PlanId == newPlan.PlanId)
            throw new InvalidOperationException("You are already subscribed to this plan.");

        Plan = newPlan;
        ExpiredDate = CalculateExpirationDate(newPlan);

        if (SubscriptionStatus != ESubscriptionStatus.COMPLETED)
            SubscriptionStatus = ESubscriptionStatus.COMPLETED;

        if (Account != null && Account.Status == EAccountStatus.INACTIVE)
            Account.ActiveAccount();
    }
}