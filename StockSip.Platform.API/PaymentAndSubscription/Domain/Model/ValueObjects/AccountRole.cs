namespace StockSip.Platform.API.PaymentAndSubscription.Domain.Model.ValueObjects;

public record AccountRole()
{
    public string Role { get; init; }
    public static AccountRole LiquorStoreOwner => new("Liquor Store Owner");
    public static AccountRole Provider => new("Provider");
    
    public AccountRole(string role) : this()
    {
        if (role != LiquorStoreOwner.Role && role != Provider.Role)
        {
            throw new ArgumentException("Invalid role type");
        }
        Role = role;
    }
}