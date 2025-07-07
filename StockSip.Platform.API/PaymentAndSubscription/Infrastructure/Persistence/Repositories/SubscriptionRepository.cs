using Microsoft.EntityFrameworkCore;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Aggregates;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.ValueObjects;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Repositories;
using StockSip.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using StockSip.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace StockSip.Platform.API.PaymentAndSubscription.Infrastructure.Persistence.Repositories;

/// <summary>
/// The SubscriptionRepository class implements the ISubscriptionRepository interface.
/// </summary>
public class SubscriptionRepository(AppDbContext context) : BaseRepository<Subscription>(context), ISubscriptionRepository
{
    public async Task<Subscription?> FindLatestPlanByAccountIdAsync(string accountId)
    {
        return await Context.Set<Subscription>()
            .Include(s => s.Plan)
            .Where(s => s.AccountId == accountId)
            .OrderByDescending(s => s.CreatedDate)
            .FirstOrDefaultAsync();
        
    }

    public async Task<string?> FindPlanIdByAccountIdAsync(string accountId)
    {
        return await Context.Set<Subscription>()
            .Where(s => s.AccountId == accountId && s.SubscriptionStatus == ESubscriptionStatus.COMPLETED)
            .OrderByDescending(s => s.CreatedDate)
            .Select(s => s.Plan.PlanId)
            .FirstOrDefaultAsync();
    }
}