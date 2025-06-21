namespace StockSip.Platform.API.payment_and_subscription.domain.ValueObjects;

public record Plan()
{
    public string PlanType { get; private set; }

    public Plan(string planType) : this()
    {
        
    }
}
