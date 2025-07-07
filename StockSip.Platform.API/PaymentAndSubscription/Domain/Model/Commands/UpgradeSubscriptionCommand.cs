namespace StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Commands;

/// <summary>
/// Command to upgrade a subscription.
/// </summary>
public record UpgradeSubscriptionCommand(string AccountId, string PlanId);