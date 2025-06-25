using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Aggregates;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Queries;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Repositories;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Services;

namespace StockSip.Platform.API.PaymentAndSubscription.Application.Internal.QueryService;

/// <summary>
/// This class implements the IAccountQueryService interface.
/// </summary>
/// <param name="accountRepository">The repository used to access account data.</param>
public class AccountQueryService(IAccountRepository accountRepository) : IAccountQueryService
{
    /// <summary>
    /// This method retrieves an account based on the provided query.
    /// </summary>
    /// <param name="query">The query containing the account ID to be retrieved.</param>
    /// <returns>The account with the specified ID, or null if not found.</returns>
    public async Task<Account?> Handle(GetAccountByIdQuery query)
    {
        return await accountRepository.FindByIdAsync(query.AccountId);
    }
}