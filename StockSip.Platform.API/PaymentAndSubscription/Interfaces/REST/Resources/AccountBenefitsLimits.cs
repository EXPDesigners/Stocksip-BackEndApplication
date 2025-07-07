namespace StockSip.Platform.API.PaymentAndSubscription.Interfaces.REST.Resources;

/// <summary>
/// This record represents the limits and benefits associated with an account's subscription.
/// </summary>
public record AccountBenefitsLimits(int MaxWarehouses, int MaxProducts);