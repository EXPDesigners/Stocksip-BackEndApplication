namespace StockSip.Platform.API.ProfileManagement.Domain.Model.ValueObjects;

public record UserBusinessName(string BusinessName)
{
    public UserBusinessName() : this(string.Empty) {}
}