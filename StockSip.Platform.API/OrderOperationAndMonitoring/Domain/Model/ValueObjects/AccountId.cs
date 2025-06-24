namespace StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.ValueObjects;

/// This value object represents the profile ID of a user or entity that will receive alerts and notifications.
/// <summary>
/// This record defines the identifier of a profile that will receive alerts and notifications.
/// </summary>
public record AccountId()
{
    /// <summary>
    /// The unique identifier for the profile.
    /// </summary>
    public string Id { get; }
    
    /// <summary>
    /// The default constructor for the AccountId record.
    /// </summary>
    /// <param name="id">The unique identifier for the account. </param>
    /// <exception cref="ArgumentException"> Account Id must be non-negative integer</exception>
    public AccountId(string id) : this()
    {
        if (id == null || id.Trim().Length == 0)
        {
            throw new ArgumentException("Account ID must be a non-empty string.");
        }
        Id = id;
    }
}