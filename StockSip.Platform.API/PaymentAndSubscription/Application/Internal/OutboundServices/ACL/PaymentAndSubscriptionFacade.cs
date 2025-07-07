using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Aggregates;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Repositories;
using StockSip.Platform.API.PaymentAndSubscription.Interfaces.ACL;

namespace StockSip.Platform.API.PaymentAndSubscription.Application.Internal.OutboundServices.ACL;

/// <summary>
/// Defines the facade for payment and subscription operations.
/// </summary>
/// <param name="accountRepository">A repository for account operations.</param>
public class PaymentAndSubscriptionFacade(IAccountRepository accountRepository) : IPaymentAndSubscriptionFacade
{
    /// <summary>
    /// Defines the method to get an account ID by user ID asynchronously.
    /// </summary>
    /// <param name="userId">The user ID to search for.</param>
    /// <returns>The account ID associated with the user ID, or null if not found.</returns>
    public async Task<string?> GetAccountIdByUserIdAsync(string userId)
    {
        return await accountRepository.FindByUserIdAsync(userId);
    }
    
    public async Task<string?> GetAccountRoleByAccountIdAsync(string accountId)
    {
        var account = await accountRepository.FindByIdAsync(accountId);
        return account?.AccountRole?.ToString();
    }
}