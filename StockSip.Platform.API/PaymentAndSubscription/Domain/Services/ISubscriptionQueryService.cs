using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Aggregates;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Queries;

namespace StockSip.Platform.API.PaymentAndSubscription.Domain.Services;

public interface ISubscriptionQueryService
{
    Task<string?> Handle(GetPlanIdByAccountIdQuery query);

    Task<(int, int)> Handle(GetLimitsByAccountId query);
}