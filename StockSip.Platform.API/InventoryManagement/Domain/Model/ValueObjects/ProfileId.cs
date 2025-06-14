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
    private int Id { get; }
    
    /// <summary>
    /// The default constructor for the ProfileId record.
    /// </summary>
    /// <param name="id">The unique identifier for the profile</param>
    /// <exception cref="ArgumentOutOfRangeException">Profile Id must be non-negative integer</exception>
    public ProfileId(int id) : this()
    {
        if (!IsValidId(id))
        {
            throw new ArgumentOutOfRangeException("Profile ID must be a non-negative integer.");
        }
        Id = id;
    }
    
    /// <summary>
    /// This method checks if the provided profile ID is valid.
    /// </summary>
    /// <param name="id">The unique identifier for the profile</param>
    /// <returns>A boolean</returns>
    private static bool IsValidId(int id) => id >= 0;
}