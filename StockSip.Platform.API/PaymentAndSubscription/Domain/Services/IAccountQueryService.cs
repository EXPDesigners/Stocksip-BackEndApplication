using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Aggregates;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Queries;

namespace StockSip.Platform.API.PaymentAndSubscription.Domain.Services;

/// <summary>
/// This interface defines the contract for account query services.
/// </summary>
public interface IAccountQueryService
{
    Task<IEnumerable<Account>> Handle(GetAccountByIdQuery query);
}