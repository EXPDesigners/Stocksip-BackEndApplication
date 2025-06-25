using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Aggregates;
using StockSip.Platform.API.Shared.Domain.Repositories;

namespace StockSip.Platform.API.PaymentAndSubscription.Domain.Repositories;

/// <summary>
/// This contract defines the repository for managing Subscription entities.
/// </summary>
public interface ISubscriptionRepository : IBaseRepository<Subscription>
{
    
}