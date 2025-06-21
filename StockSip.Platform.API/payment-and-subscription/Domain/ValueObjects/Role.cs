namespace StockSip.Platform.API.payment_and_subscription.domain.ValueObjects;

/// <summary>
/// This record represents the type of role assigned in an account.
/// It can be: Liquor Store Owner | Provider 
/// </summary>
public record Role()
{
    public string RoleType { get; private set; }
    
    public Role(string roleType) : this()
    {
        RoleType = roleType;
    }
}