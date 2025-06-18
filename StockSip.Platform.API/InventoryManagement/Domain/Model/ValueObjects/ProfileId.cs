namespace StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;

/// This value object represents the profile ID of a warehouse.
/// <summary>
/// This record defines the profile ID for a warehouse.
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
        Id = id ?? throw new ArgumentException("Profile Id cannot be null.");
    }
}