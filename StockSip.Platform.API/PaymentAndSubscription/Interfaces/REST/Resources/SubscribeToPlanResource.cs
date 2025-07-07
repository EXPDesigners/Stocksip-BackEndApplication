namespace StockSip.Platform.API.PaymentAndSubscription.Interfaces.REST.Resources;

/// <summary>
/// This resource represents a request to subscribe to a specific plan for an account.
/// </summary>
/// <param name="AccountId">The unique identifier of the account subscribing to the plan.</param>
/// <param name="PlanId">The unique identifier of the plan to which the account is subscribing.</param>
public record SubscribeToPlanResource(string AccountId, string PlanId);