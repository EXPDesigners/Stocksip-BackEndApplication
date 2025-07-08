namespace StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Commands;

/// <summary>
/// This command is used to subscribe an account to a specific plan.
/// </summary>
/// <param name="AccountId">The unique identifier of the account to be subscribed.</param>
/// <param name="PlanId">The unique identifier of the plan to which the account is subscribing.</param>
public record SubscribeToPlanCommand(string AccountId, string PlanId);