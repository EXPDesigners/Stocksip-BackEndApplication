namespace StockSip.Platform.API.ProfileManagement.Domain.Model.ValueObjects;

public record UserPhoneNumber(string PhoneNumber)
{
    public UserPhoneNumber() : this(string.Empty) {}
}

