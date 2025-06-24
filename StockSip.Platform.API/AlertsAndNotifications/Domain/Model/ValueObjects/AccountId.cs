namespace StockSip.Platform.API.AlertsAndNotifications.Domain.Model.ValueObjects;

/// <summary>
/// This record defines the identifier of an account that will receive alerts and notifications.
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
    /// <param name="id">The unique identifier for the account</param>
    /// <exception cref="ArgumentException"> Account ID must be a non-empty string. </exception>
    public AccountId(string id) : this()
    {
        if (id == null || id.Trim().Length == 0)
        {
            throw new ArgumentException("Account ID must be a non-empty string.");
        }
        Id = id;
    }
}