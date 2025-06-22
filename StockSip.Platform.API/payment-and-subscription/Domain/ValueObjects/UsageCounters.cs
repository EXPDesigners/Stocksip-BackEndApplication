namespace StockSip.Platform.API.payment_and_subscription.domain.ValueObjects;

public record UsageCounters()
{
    public int CountWarehouses { get; init; }
    public int CountProducts { get; init; }
    
}