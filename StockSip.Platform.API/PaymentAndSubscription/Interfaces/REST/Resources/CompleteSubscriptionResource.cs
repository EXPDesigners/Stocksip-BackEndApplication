namespace StockSip.Platform.API.PaymentAndSubscription.Interfaces.REST.Resources;

/// <summary>
/// This record represents the resource for completing a subscription.
/// </summary>
/// <param name="Token">The token received from the payment gateway.</param>
/// <param name="AccountId">The unique identifier of the account associated with the subscription.</param>
/// <param name="PlanId">The unique identifier of the subscription plan being subscribed to.</param>
public record CompleteSubscriptionResource
{
    public string Token { get; init; }
    public string AccountId { get; init; }
    public string PlanId { get; init; }
}