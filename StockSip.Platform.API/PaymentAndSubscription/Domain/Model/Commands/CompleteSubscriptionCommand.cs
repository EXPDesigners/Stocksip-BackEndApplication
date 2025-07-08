namespace StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Commands;

/// <summary>
/// This record represents a command to complete a subscription.
/// </summary>
/// <param name="Token">The token received from the payment gateway.</param>
/// <param name="AccountId">The unique identifier of the account subscribing to the plan.</param>
/// <param name="PlanId">The unique identifier of the plan being subscribed to.</param>
public record CompleteSubscriptionCommand(string Token, string AccountId, string PlanId);