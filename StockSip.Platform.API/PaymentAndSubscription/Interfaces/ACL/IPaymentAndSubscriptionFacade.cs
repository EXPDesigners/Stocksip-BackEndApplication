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
}