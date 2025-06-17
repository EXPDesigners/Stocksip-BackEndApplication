namespace StockSip.Platform.API.ProfileManagement.Domain.Model.ValueObjects;

public record UserRole(string Role)
{
    public UserRole() : this(string.Empty) {}
    public static UserRole LiquorStoreOwner => new("Liquor Store Owner");
    public static UserRole Supplier => new("Supplier");
}