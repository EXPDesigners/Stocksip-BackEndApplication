namespace StockSip.Platform.API.PaymentAndSubscription.Interfaces.REST.Resources;

/// <summary>
/// This record represents the resource for completing an upgrade to a subscription plan.
/// </summary>
public record CompleteUpgradeResource
{
    public string Token { get; init; }
    public string AccountId { get; init; }
    public string PlanId { get; init; }
}
