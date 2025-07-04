using Microsoft.EntityFrameworkCore;
using StockSip.Platform.API.Authorization.Domain.Model.Aggregate;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Aggregates;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Repositories;
using StockSip.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using StockSip.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace StockSip.Platform.API.PaymentAndSubscription.Infrastructure.Repositories;

/// <summary>
/// The AccountRepository class implements the IAccountRepository interface
/// </summary>
public class AccountRepository(AppDbContext context) : BaseRepository<Account>(context), IAccountRepository
{
    public async Task<string?> FindByUserIdAsync(string userId)
    {
        var account = await Context.Set<Account>()
            .FirstOrDefaultAsync(account => account.OwnerUserId.OwnerUserId == userId);

        return account?.AccountId;
    }
}