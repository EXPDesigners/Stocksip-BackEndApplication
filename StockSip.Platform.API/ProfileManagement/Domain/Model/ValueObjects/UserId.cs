namespace StockSip.Platform.API.ProfileManagement.Domain.Model.ValueObjects;

/// <summary>
/// This value object represents the user ID of a profile.
/// </summary>
public record UserId
{
    /// <summary>
    /// The unique identifier for the user profile.
    /// </summary>
    public string Id { get; }
    
    /// <summary>
    /// The default constructor for the UserId record.
    /// </summary>
    /// <param name="id">
    /// The unique identifier for the user profile.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown when the provided ID is null, empty, or consists only of whitespace.
    /// </exception>
    public UserId(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("User ID must be a non-empty string.", nameof(id));
        }
        Id = id;
    }
}