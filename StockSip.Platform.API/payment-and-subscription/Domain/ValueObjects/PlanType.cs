namespace StockSip.Platform.API.payment_and_subscription.domain.ValueObjects;

public record PlanType(string Type)
{
    public static PlanType Free => new("Free");
    public static PlanType Premium => new("Premium");
    
    public bool IsPremium => this == Premium;
}