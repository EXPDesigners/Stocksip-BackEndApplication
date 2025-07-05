using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Aggregates;
using StockSip.Platform.API.Shared.Domain.Repositories;

namespace StockSip.Platform.API.PaymentAndSubscription.Domain.Repositories;

/// <summary>
/// This interface defines the repository for managing Account entities.
/// </summary>
public interface IAccountRepository : IBaseRepository<Account>
{
    Task<Account?> FindByIdAsync(string accountId);
    Task<Account?> FindByEmailAsync(string email);
}