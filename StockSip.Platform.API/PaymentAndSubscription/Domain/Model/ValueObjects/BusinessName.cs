namespace StockSip.Platform.API.PaymentAndSubscription.Domain.Model.ValueObjects;

/// <summary>
/// This value object represents the name of the business in the account.
/// </summary>
public record BusinessName()
{
    public string Name { get; private set; }
    
    public BusinessName(string name) : this()
    {
        if (string.IsNullOrWhiteSpace(name) || name.Length < 1)
        {
            throw new ArgumentException("Business name must be at least 1 characters long.");
        }
        
        Name = name.Trim();
    }
    
}