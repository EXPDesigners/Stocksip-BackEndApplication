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
    public int Id { get; }
    
    /// <summary>
    /// The default constructor for the ProfileId record.
    /// </summary>
    /// <param name="id">The unique identifier for the profile</param>
    /// <exception cref="ArgumentException">Profile Id must be non-negative integer</exception>
    public ProfileId(int id) : this()
    {
        if (id < 0)
        {
            throw new ArgumentException("Profile ID must be a non-negative integer.");
        }
        Id = id;
    }
    
        
    /// <summary>
    /// This method transforms the ProfileId to a string representation.
    /// </summary>
    public static implicit operator ProfileId(int value) => new(value);
}