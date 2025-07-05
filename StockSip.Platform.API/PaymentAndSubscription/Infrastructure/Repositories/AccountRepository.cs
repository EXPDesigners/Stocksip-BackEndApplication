using Microsoft.EntityFrameworkCore;
using StockSip.Platform.API.Authorization.Domain.Model.Aggregate; // Para OwnerUserId, si aplica
using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Aggregates;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Repositories;
using StockSip.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using StockSip.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace StockSip.Platform.API.PaymentAndSubscription.Infrastructure.Repositories;

/// <summary>
/// Implementa <see cref="IAccountRepository"/> ofreciendo métodos de búsqueda por:
/// • AccountId<br/>
/// • Email<br/>
/// • UserId (propietario externo)<br/>
/// </summary>
public class AccountRepository(AppDbContext context)
    : BaseRepository<Account>(context), IAccountRepository
{
    private DbSet<Account> Accounts => Context.Set<Account>();

    /// <summary>
    /// Devuelve la entidad <see cref="Account"/> cuyo <c>AccountId</c> coincide.
    /// </summary>
    public async Task<Account?> FindByIdAsync(string accountId) =>
        await Accounts.FirstOrDefaultAsync(a => a.AccountId == accountId);

    /// <summary>
    /// Devuelve la entidad <see cref="Account"/> cuyo email coincide.
    /// </summary>
    public async Task<Account?> FindByEmailAsync(string email) =>
        await Accounts.FirstOrDefaultAsync(a => a.Email.Value == email);

    /// <summary>
    /// Devuelve el <c>AccountId</c> asociado al <paramref name="userId"/> externo, o <c>null</c> si no existe.
    /// </summary>
    public async Task<string?> FindByUserIdAsync(string userId)
    {
        var account = await Accounts
            .FirstOrDefaultAsync(a => a.OwnerUserId.OwnerUserId == userId);

        return account?.AccountId;
    }
}