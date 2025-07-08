namespace StockSip.Platform.API.ProfileManagement.Domain.Model.ValueObjects;

/// <summary>
/// This value object represents the account identifier.
/// </summary>
public record AccountId()
{
    /// <summary>
    /// The unique identifier for the account.
    /// </summary>
    public string Id { get; }
    
    /// <summary>
    /// The default constructor for the AccountId record.
    /// </summary>
    /// <param name="id">
    /// The unique identifier for the account.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown when the provided ID is null, empty, or consists only of whitespace.
    /// </exception>
    public AccountId(string id) : this()
    {
        if (string.IsNullOrWhiteSpace(id))
        {
            throw new ArgumentException("Account ID must be a non-empty string.", nameof(id));
        }
        Id = id;
    }
}