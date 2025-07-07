using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Queries;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Repositories;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Services;

namespace StockSip.Platform.API.PaymentAndSubscription.Application.Internal.QueryService;

public class SubscriptionQueryService(ISubscriptionRepository subscriptionRepository) : ISubscriptionQueryService
{
    public async Task<string?> Handle(GetPlanIdByAccountIdQuery query)
    {
        return await subscriptionRepository.FindPlanIdByAccountIdAsync(query.accountId);
    }

    public async Task<(int, int)> Handle(GetLimitsByAccountId query)
    {
        return await subscriptionRepository.FindLimitsByAccountIdAsync(query.accountId);
    }
}