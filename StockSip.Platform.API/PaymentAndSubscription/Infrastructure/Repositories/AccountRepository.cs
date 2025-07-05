using Microsoft.EntityFrameworkCore;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Aggregates;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Repositories;
using StockSip.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using StockSip.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace StockSip.Platform.API.PaymentAndSubscription.Infrastructure.Repositories;

/// <summary>
/// The AccountRepository class implements the IAccountRepository interface
/// </summary>
public class AccountRepository(AppDbContext context)
    : BaseRepository<Account>(context), IAccountRepository
{
    private DbSet<Account> Accounts => Context.Set<Account>();

    public async Task<Account?> FindByIdAsync(string accountId) =>
        await Accounts.FirstOrDefaultAsync(a => a.AccountId == accountId);

    public async Task<Account?> FindByEmailAsync(string email) =>
        await Accounts.FirstOrDefaultAsync(a => a.Email.Value == email);
}