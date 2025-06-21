namespace StockSip.Platform.API.payment_and_subscription.domain.ValueObjects;

public record GeneralEmail()
{
    public string Email { get; private set; }

    public GeneralEmail(string email) : this()
    {
        if (string.IsNullOrWhiteSpace(email))
        {
            throw new ArgumentException("Email cannot be null or empty.", nameof(email));
        }

        Email = email;
    }
    
}
