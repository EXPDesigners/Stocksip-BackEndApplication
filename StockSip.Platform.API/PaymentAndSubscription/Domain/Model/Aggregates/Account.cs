using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Commands;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Aggregates;

/// This aggregate represents an account in the payment and subscription domain.
/// <summary>
/// This class encapsulates the properties and behaviors of an account, including its unique identifier and associated email.
/// </summary>
public class Account 
{
    public string AccountId { get; private set; }
    
    public BusinessName BusinessName { get; internal set; }
    
    public EmailAddress Email { get; internal set; }
    
    public EAccountStatus Status { get; set; }
    
    public AccountRole AccountRole { get; internal set; }
    
    public DateTime CreatedDate { get; internal set; }
    
    public UserId OwnerUserId { get; internal set; }
    
    /// <summary>
    /// Default constructor for EF Core.
    /// </summary>
    protected Account() {}

    /// <summary>
    /// Constructor to create a new account with the specified owner user ID, account role, and address.
    /// </summary>
    public Account(string ownerUserId, string businessName, string email, string accountRole)
    {
        AccountId = Guid.NewGuid().ToString();
        OwnerUserId = new UserId(ownerUserId);
        BusinessName = new BusinessName(businessName);
        Email = new EmailAddress(email);
        AccountRole = new AccountRole(accountRole);
        CreatedDate = DateTime.UtcNow;
        Status = EAccountStatus.INACTIVE;
    }
    
    public Account(CreateAccountCommand cmd)
    {
        AccountId     = Guid.NewGuid().ToString();
        Email         = new EmailAddress(cmd.Email);
        OwnerUserId   = new UserId(cmd.OwnerUserId);
        AccountRole   = new AccountRole(cmd.AccountRole);
        BusinessName  = new BusinessName(cmd.BusinessName);
        CreatedDate   = DateTime.UtcNow;
        Status        = EAccountStatus.INACTIVE;
    }


    /// <summary>
    /// This method is used to activate the account, changing its status to ACTIVE.
    /// </summary>
    public void ActiveAccount()
    {
        Status = EAccountStatus.ACTIVE;
    }
    
    public string GetCreationDate()
    {
        return CreatedDate.ToString("yyyy-M-d");
    }
}