using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Aggregates;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Repositories;
using StockSip.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using StockSip.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace StockSip.Platform.API.PaymentAndSubscription.Infrastructure.Repositories;

/// <summary>
/// The SubscriptionRepository class implements the ISubscriptionRepository interface.
/// </summary>
public class SubscriptionRepository(AppDbContext context) : BaseRepository<Subscription>(context), ISubscriptionRepository
{
    
}