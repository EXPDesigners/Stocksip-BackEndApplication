using Microsoft.EntityFrameworkCore;
using StockSip.Platform.API.Authorization.Domain.Model.Aggregate; // Para OwnerUserId, si aplica
using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Aggregates;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Repositories;
using StockSip.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using StockSip.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace StockSip.Platform.API.PaymentAndSubscription.Infrastructure.Persistence.Repositories;

/// <summary>
/// Implements <see cref="IAccountRepository"/> using Entity Framework Core.
public class AccountRepository(AppDbContext context)
    : BaseRepository<Account>(context), IAccountRepository
{
    private DbSet<Account> Accounts => Context.Set<Account>();

    /// <summary>
    /// Retrieves the <see cref="Account"/> entity by its unique identifier.
    /// </summary>
    /// <param name="accountId">The unique identifier of the account to be retrieved.</param>
    /// <returns>The <see cref="Account"/> entity if found; otherwise, null.</returns>
    public async Task<Account?> FindByIdAsync(string accountId) =>
        await Accounts.FirstOrDefaultAsync(a => a.AccountId == accountId);

    /// <summary>
    /// Retrieves the <see cref="Account"/> entity by its email address.
    /// </summary>
    /// <param name="email">The email address of the account to be retrieved.</param>
    /// <returns>The <see cref="Account"/> entity if found; otherwise, null.</returns>
    public async Task<Account?> FindByEmailAsync(string email) =>
        await Accounts.FirstOrDefaultAsync(a => a.Email.Value == email);

    /// <summary>
    /// Finds the account associated with a specific user ID.
    /// </summary>
    public async Task<string?> FindByUserIdAsync(string userId)
    {
        var account = await Accounts
            .FirstOrDefaultAsync(a => a.OwnerUserId.OwnerUserId == userId);

        return account?.AccountId;
    }

    public async Task<string?> FindStatusByAccountId(string accountId)
    {
        var account = await Context.Set<Account>()
            .FirstOrDefaultAsync(account => account.AccountId == accountId);

        return account?.Status.ToString();
    }
}