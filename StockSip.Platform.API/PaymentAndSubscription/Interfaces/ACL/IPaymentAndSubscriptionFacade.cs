using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Aggregates;

namespace StockSip.Platform.API.PaymentAndSubscription.Interfaces.ACL;

/// <summary>
/// This interface defines the contract for the Payment and Subscription Facade.
/// </summary>
public interface IPaymentAndSubscriptionFacade
{
    /// <summary>
    /// This method is used to create a new user account with the provided username and password.
    /// </summary>
    Task<string?> GetAccountIdByUserIdAsync(string userId);

    /// <summary>
    /// Retrieves the role associated with a specific account ID.
    /// </summary>
    /// <param name="accountId">The unique identifier of the account for which the role is to be retrieved.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the account role as a string, or null if no role is found for the given account ID.</returns>
    Task<string?> GetAccountRoleByAccountIdAsync(string accountId);
    
    /// <summary>
    /// This method retrieves the limits for warehouses and products associated with a specific account ID.
    /// </summary>
    /// <param name="accountId">The unique identifier of the account for which the limits are to be retrieved.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains a tuple with the maximum number of warehouses and products allowed for the account.</returns>
    Task<(int MaxWarehouses, int MaxProducts)> GetLimitsByAccountIdAsync(string accountId); 
}