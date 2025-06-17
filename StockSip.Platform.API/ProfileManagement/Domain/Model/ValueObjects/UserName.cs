namespace StockSip.Platform.API.ProfileManagement.Domain.Model.ValueObjects;

public record UserName(string Name) 
{
    public UserName() : this(string.Empty) {}
    
}