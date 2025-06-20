namespace StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;

/// This value object represents the profile ID of a user or entity that will receive alerts and notifications.
/// <summary>
/// This record defines the identifier of a profile that will receive alerts and notifications.
/// </summary>
public record ProfileId()
{
    /// <summary>
    /// The unique identifier for the profile.
    /// </summary>
    public string Id { get; }
    
    /// <summary>
    /// The default constructor for the ProfileId record.
    /// </summary>
    /// <param name="id">The unique identifier for the profile</param>
    /// <exception cref="ArgumentException">Profile Id must be non-negative integer</exception>
    public ProfileId(string id) : this()
    {
        if (id == null || id.Trim().Length == 0)
        {
            throw new ArgumentException("Profile ID must be a non-empty string.");
        }
        Id = id;
    }
}