namespace StockSip.Platform.API.ProfileManagement.Domain.Model.ValueObjects;

/// <summary>
/// This value object represents the person's last name.
/// </summary>
public record PersonLastName()
{
    /// <summary>
    /// The last name of the profile.
    /// </summary>
    public string LastName { get; }

    /// <summary>
    /// The default constructor for the PersonLastName record.
    /// </summary>
    /// <param name="lastName">The last name of the profile.</param>
    /// <exception cref="ArgumentException">Thrown when the last name is null or empty.</exception>
    public PersonLastName(string lastName) : this()
    {
        if (string.IsNullOrWhiteSpace(lastName))
        {
            throw new ArgumentException("Last name must be a non-empty string.", nameof(lastName));
        }
        LastName = lastName;
    }
}