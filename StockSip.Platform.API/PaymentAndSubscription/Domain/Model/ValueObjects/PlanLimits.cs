namespace StockSip.Platform.API.PaymentAndSubscription.Domain.Model.ValueObjects;

/// <summary>
/// This value object represents the limits of a subscription plan.
/// </summary>
/// <param name="MaxWarehouses">=The maximum number of warehouses allowed in the plan.</param>
/// <param name="MaxProducts">The maximum number of products allowed in the plan.</param>
public record PlanLimits(int MaxWarehouses, int MaxProducts);