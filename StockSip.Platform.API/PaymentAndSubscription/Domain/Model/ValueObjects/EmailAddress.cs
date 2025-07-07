
namespace StockSip.Platform.API.PaymentAndSubscription.Domain.Model.ValueObjects;

public sealed record EmailAddress
{
    public string Value { get; }

    public EmailAddress(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Email cannot be null or empty.", nameof(value));

        value = value.Trim().ToLowerInvariant();
        
        if (!value.Contains('@') || !value.Contains('.'))
            throw new ArgumentException("Invalid email format.", nameof(value));

        Value = value;
    }

    public override string ToString() => Value;
}