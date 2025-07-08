namespace StockSip.Platform.API.PaymentAndSubscription.Interfaces.REST.Resources;

/// <summary>
/// This record represents a command to upgrade a subscription.
/// </summary>
public record UpgradeSubscriptionResource(string AccountId, string PlanId);