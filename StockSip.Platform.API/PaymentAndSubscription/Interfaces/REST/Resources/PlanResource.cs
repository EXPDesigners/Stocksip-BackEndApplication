namespace StockSip.Platform.API.PaymentAndSubscription.Interfaces.REST.Resources;

/// <summary>
/// This class represents a resource for a subscription plan.
/// </summary>
public record PlanResource(string PlanId, 
                           string PlanType,
                           string Description,
                           decimal Price,
                           int MaxWarehouses,
                           int MaxProducts);