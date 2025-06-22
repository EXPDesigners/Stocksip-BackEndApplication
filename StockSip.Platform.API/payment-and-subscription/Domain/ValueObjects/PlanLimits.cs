namespace StockSip.Platform.API.payment_and_subscription.domain.ValueObjects;

public record PlanLimits()
{
    public int MaxWarehouses { get; }
    
    public int MaxProducts { get; }
}