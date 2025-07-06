namespace StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.ValueObjects;

/// This value object represents the profile ID of a user or entity that will receive alerts and notifications.
/// <summary>
/// This record defines the identifier of a profile that will receive alerts and notifications.
/// </summary>
public record AccountId
{
    
    public string Value { get; init; } = null!; 


    private AccountId() { }
    
    public AccountId(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("AccountId cannot be empty.", nameof(value));

        Value = value;
    }

    public override string ToString() => Value;
    public static implicit operator string(AccountId id) => id.Value;
}