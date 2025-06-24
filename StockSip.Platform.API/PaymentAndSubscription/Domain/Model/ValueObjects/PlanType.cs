namespace StockSip.Platform.API.PaymentAndSubscription.Domain.Model.ValueObjects;

public record PlanType(string Type)
{
    public static PlanType FREE => new("FREE");
    public static PlanType PREMIUM => new("Premium");
}