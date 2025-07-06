namespace StockSip.Platform.API.PaymentAndSubscription.Domain.Model.ValueObjects;

public record AccountRole
{
    public string Role { get; }

    public static readonly AccountRole Supplier          = new("Supplier");
    public static readonly AccountRole LiquorStoreOwner  = new("Liquor Store Owner");

    public AccountRole(string role)
    {
        if (string.IsNullOrWhiteSpace(role))
            throw new ArgumentException("Role cannot be empty.", nameof(role));
        
        Role = role.Trim();
        
    }

    public override string ToString() => Role;
}
