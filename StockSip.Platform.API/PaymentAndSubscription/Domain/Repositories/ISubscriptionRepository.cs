using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Aggregates;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Queries;
using StockSip.Platform.API.Shared.Domain.Repositories;

namespace StockSip.Platform.API.PaymentAndSubscription.Domain.Repositories;

/// <summary>
/// This contract defines the repository for managing Subscription entities.
/// </summary>
public interface ISubscriptionRepository : IBaseRepository<Subscription>
{
    /// <summary>
    /// This method retrieves the latest subscription plan for a given account ID.
    /// </summary>
    /// <returns>The latest subscription plan ID for the specified account, or null if no plan is found.</returns>
    Task<Subscription?> FindLatestPlanByAccountIdAsync(string accountId);
    
    Task<string?> FindPlanIdByAccountIdAsync(string accountId);
    
    Task<Subscription?> FindByAccountIdAsync(string accountId);
    
    Task<(int, int)> FindLimitsByAccountIdAsync(string accountId);
}