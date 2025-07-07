namespace StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Commands;

/// <summary>
/// This command is used to complete the upgrade process for a subscription.
/// </summary>
public record CompleteUpgradeCommand(string Token, string AccountId, string PlanId);