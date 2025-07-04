namespace StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Commands;

/// <summary>
/// This command is used to create a new subscription between an account and a plan.
/// </summary>
/// <param name="AccountId">The unique identifier of the account that is subscribing.</param>
/// <param name="PlanId">The unique identifier of the plan to which the account is subscribing.</param>
public record CreateSubscriptionCommand(string AccountId, string PlanId);