namespace StockSip.Platform.API.ProfileManagement.Domain.Model.ValueObjects;

public record UserEmail(string Email)
{
    public UserEmail() : this(string.Empty) {}
}
