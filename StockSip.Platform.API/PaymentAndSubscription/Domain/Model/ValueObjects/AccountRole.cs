namespace StockSip.Platform.API.PaymentAndSubscription.Domain.Model.ValueObjects;

public record AccountRole()
{
    public string Type { get; init; }
    public static AccountRole LiquorStoreOwner => new("Liquor Store Owner");
    public static AccountRole Provider => new("Provider");
    
    public AccountRole(string type) : this()
    {
        if (type != LiquorStoreOwner.Type && type != Provider.Type)
        {
            throw new ArgumentException("Invalid role type");
        }
        Type = type;
    }
}