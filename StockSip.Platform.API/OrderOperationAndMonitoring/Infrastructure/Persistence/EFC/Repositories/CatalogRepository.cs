using Microsoft.EntityFrameworkCore;
using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.Aggregates;
using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.ValueObjects;
using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Repositories;
using StockSip.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using StockSip.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace StockSip.Platform.API.OrderOperationAndMonitoring.Infrastructure.Persistence.EFC.Repositories;

public class CatalogRepository(AppDbContext context)
    : BaseRepository<Catalog>(context), ICatalogRepository
{
    private DbSet<Catalog> Catalogs => Context.Set<Catalog>();

    public async Task<IEnumerable<Catalog>> FindByAccountIdAsync(AccountId accountId)
    {
        return await Catalogs
            .Where(c => c.AccountId.Value == accountId.Value)   
            .ToListAsync();
    }

    public async Task<IEnumerable<Catalog>> FindPublishedByAccountIdAsync(AccountId accountId)
    {
        return await Catalogs
            .Where(c => c.AccountId == accountId && c.IsPublished)
            .ToListAsync();
    }

    public async Task<IEnumerable<Catalog>> FindPublishedAsync()
    {
        return await Catalogs
            .Where(c => c.IsPublished)
            .ToListAsync();
    }

    public async Task<IEnumerable<CatalogItem>> FindByCatalogIdAsync(long catalogId)
    {
        var catalog = await Catalogs
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.CatalogId == catalogId);

        return catalog?.Items ?? Enumerable.Empty<CatalogItem>();
    }
    
    public Task<Catalog?> FindByIdAsync(long id, bool includeItems = false)
    {
        IQueryable<Catalog> query = Context.Set<Catalog>();
        if (includeItems) query = query.Include(c => c.Items);

        return query.FirstOrDefaultAsync(c => c.CatalogId == id);
    }


    public async Task<Catalog?> FindCatalogByItemIdAsync(string itemId) =>
        await Catalogs
            .Include(c => c.Items)
            .FirstOrDefaultAsync(c => c.Items.Any(i => i.Id == itemId));
}