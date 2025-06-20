namespace StockSip.Platform.API.AlertsAndNotifications.Domain.Model.ValueObjects;

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
    /// <exception cref="ArgumentException"> Profile ID must be a non-empty string. </exception>
    public ProfileId(string id) : this()
    {
        if (id == null || id.Trim().Length == 0)
        {
            throw new ArgumentException("Profile ID must be a non-empty string.");
        }
        Id = id;
    }
}