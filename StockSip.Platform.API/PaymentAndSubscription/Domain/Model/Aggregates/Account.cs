using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Commands;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Entities;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Aggregates;

/// This aggregate represents an account in the payment and subscription domain.
/// <summary>
/// This class encapsulates the properties and behaviors of an account, including its unique identifier and associated email.
/// </summary>
public class Account 
{
    public string AccountId { get; private set; }
    
    public AccountStatus AccountStatus { get; internal set; }
    
    public Role Role { get; internal set; }
    
    public List<Subscription> Subscriptions { get; set; } = new();
    
    public DateTime CreatedDate { get; internal set; }
    
    public UserId UserId { get; internal set; }
    
    /// <summary>
    /// Default constructor for EF Core.
    /// </summary>
    private Account() {}

    public Account(CreateAccountCommand command)
    {
        AccountId = Guid.NewGuid().ToString();
        UserId = new UserId(command.UserId);
        Role = new Role(command.Role);
        CreatedDate = DateTime.UtcNow;
        AccountStatus = AccountStatus.INACTIVE;
    }
    
    public void Subscribe(Subscription subscription)
    {
        if (AccountStatus == AccountStatus.ACTIVE)
            throw new InvalidOperationException("Account is already active.");

        if (Subscriptions.Any(s => s.IsActive))
            throw new InvalidOperationException("There is already an active subscription.");

        Subscriptions.Add(subscription);
        AccountStatus = AccountStatus.ACTIVE;
    }


}