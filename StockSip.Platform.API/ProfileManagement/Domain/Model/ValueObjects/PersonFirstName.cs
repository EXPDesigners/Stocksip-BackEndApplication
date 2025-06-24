namespace StockSip.Platform.API.ProfileManagement.Domain.Model.ValueObjects;

/// <summary>
/// This value object represents the person's first name.
/// </summary>
public record PersonFirstName()
{
    /// <summary>
    /// The first name of the profile.
    /// </summary>
    public string FirstName { get; }

    /// <summary>
    /// The default constructor for the ProfileFirstName record.
    /// </summary>
    /// <param name="firstName">The first name of the profile.</param>
    /// <exception cref="ArgumentException">Thrown when the first name is null or empty.</exception>
    public PersonFirstName(string firstName) : this()
    {
        if (string.IsNullOrWhiteSpace(firstName))
        {
            throw new ArgumentException("First name must be a non-empty string.", nameof(firstName));
        }
        FirstName = firstName;
    }
}