using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.Aggregates;
using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.ValueObjects;
using StockSip.Platform.API.Shared.Domain.Repositories;

namespace StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Repositories;

public interface ICatalogRepository : IBaseRepository<Catalog>
{
    /// <summary>
    /// Retrieves all catalogs, regardless of publication status, associated with a specific account.
    /// </summary>
    Task<IEnumerable<Catalog>> FindByAccountIdAsync(AccountId accountId);

    /// <summary>
    /// Returns all published catalogs for a specific account.
    /// </summary>
    Task<IEnumerable<Catalog>> FindPublishedByAccountIdAsync(AccountId accountId);

    /// <summary>
    /// Retrieves all catalogs that are currently published.
    /// </summary>
    Task<IEnumerable<Catalog>> FindPublishedAsync();

    /// <summary>
    /// Retrieves all catalog items associated with a specific catalog.
    /// </summary>
    Task<IEnumerable<CatalogItem>> FindByCatalogIdAsync(long catalogId);

    /// <summary>
    /// Retrieves a catalog by its ID with optional inclusion of its items.
    /// </summary>
    /// <param name="id">Catalog ID</param>
    /// <param name="includeItems">Whether to include associated catalog items</param>
    /// <returns>The matching catalog or null</returns>
    Task<Catalog?> FindByIdAsync(long id, bool includeItems = false);

    /// <summary>
    /// Retrieves the catalog that contains a catalog item with the specified item ID.
    /// </summary>
    /// <param name="itemId">Catalog item ID</param>
    /// <returns>The catalog containing the item, or null if not found</returns>
    Task<Catalog?> FindCatalogByItemIdAsync(string itemId);
}