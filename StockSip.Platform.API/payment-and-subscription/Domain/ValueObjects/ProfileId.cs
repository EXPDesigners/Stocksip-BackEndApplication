namespace StockSip.Platform.API.payment_and_subscription.domain.ValueObjects;


public record ProfileId()
{

    public string Id { get; }
    
    public ProfileId(string id) : this()
    {
        if (id == null || id.Trim().Length == 0)
        {
            throw new ArgumentException("Profile ID must be a non-empty string.");
        }
        Id = id;
    }
}