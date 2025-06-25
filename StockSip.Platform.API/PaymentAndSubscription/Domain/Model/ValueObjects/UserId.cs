namespace StockSip.Platform.API.PaymentAndSubscription.Domain.Model.ValueObjects;

/// <summary>
/// The unique identifier for the user owner.
/// </summary>
public record UserId()
{
    public string OwnerUserId { get; }
    
    public UserId(string ownerUserId) : this()
    {
        if (ownerUserId == null || ownerUserId.Trim().Length == 0)
        {
            throw new ArgumentException("Profile ID must be a non-empty string.");
        }
        OwnerUserId = ownerUserId;
    }
}