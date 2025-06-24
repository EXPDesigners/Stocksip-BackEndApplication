namespace StockSip.Platform.API.PaymentAndSubscription.Domain.Model.ValueObjects;

/// <summary>
/// The unique identifier for the user owner.
/// </summary>
public record OwnerUserId()
{
    public string Id { get; }
    
    public OwnerUserId(string id) : this()
    {
        if (id == null || id.Trim().Length == 0)
        {
            throw new ArgumentException("Profile ID must be a non-empty string.");
        }
        Id = id;
    }
}