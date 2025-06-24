namespace StockSip.Platform.API.PaymentAndSubscription.Domain.Model.ValueObjects;

public record Role()
{
    public string Type { get; init; }
    public static Role LiquorStoreOwner => new("Liquor Store Owner");
    public static Role Provider => new("Provider");
    
    public Role(string type) : this()
    {
        if (type != LiquorStoreOwner.Type && type != Provider.Type)
        {
            throw new ArgumentException("Invalid role type");
        }
        Type = type;
    }
}