namespace StockSip.Platform.API.payment_and_subscription.domain.ValueObjects;

public record Role(string Type)
{
    public static Role LiquorStoreOwner => new("LiquorStoreOwner");
    public static Role Provider => new("Provider");
}