namespace StockSip.Platform.API.PaymentAndSubscription.Domain.Model.ValueObjects;

/// <summary>
/// The unique identifier for a user account.
/// </summary>
public record UserId()
{
    public string Id { get; }
    
    public UserId(string id) : this()
    {
        if (id == null || id.Trim().Length == 0)
        {
            throw new ArgumentException("Profile ID must be a non-empty string.");
        }
        Id = id;
    }
}