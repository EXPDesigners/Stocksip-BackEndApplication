namespace StockSip.Platform.API.ProfileManagement.Domain.Model.ValueObjects;

public record UserBusinessAddress(string Address)
{
    public UserBusinessAddress() : this(string.Empty) {}

}