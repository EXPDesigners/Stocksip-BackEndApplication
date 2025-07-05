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
    
    public EAccountStatus Status { get; set; }
    
    public EAccountRole AccountRole { get; internal set; }
    
    public DateOnly CreatedDate { get; internal set; }
    
    public UserId OwnerUserId { get; internal set; }
    
    /// <summary>
    /// Default constructor for EF Core.
    /// </summary>
    protected Account() {}

    /// <summary>
    /// Constructor to create a new account with the specified owner user ID, account role, and address.
    /// </summary>
    public Account(string ownerUserId, string accountRole, string businessName)
    {
        AccountId = Guid.NewGuid().ToString();
        BusinessName = new BusinessName(businessName);
        OwnerUserId = new UserId(ownerUserId);
        AccountRole = Enum.Parse<EAccountRole>(accountRole, true); ;
        CreatedDate = DateOnly.FromDateTime(DateTime.Now);
        Status = EAccountStatus.INACTIVE;
    }

    /// <summary>
    /// This method is used to activate the account, changing its status to ACTIVE.
    /// </summary>
    public void ActiveAccount()
    {
        Status = EAccountStatus.ACTIVE;
    }
    
    /// <summary>
    /// This method is used to deactivate the account, changing its status to INACTIVE.
    /// </summary>
    /// <returns>A boolean indicating whether the account was successfully deactivated.</returns>
    public string GetCreationDate()
    {
        return CreatedDate.ToString("yyyy-M-d");
    }
}