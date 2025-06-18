using StockSip.Platform.API.InventoryManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Entities;
using StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;
using StockSip.Platform.API.Shared.Domain.Repositories;

namespace StockSip.Platform.API.InventoryManagement.Domain.Repositories;

/// <summary>
/// This interface defines the contract for a repository that manages Product aggregates.
/// </summary>
public interface IProductRepository : IBaseRepository<Product>
{
    /// <summary>
    /// This method retrieves all products associated with a specific provider ID.
    /// </summary>
    /// <returns>
    /// An enumerable collection of Product aggregates that belong to the specified provider.
    /// </returns>
    Task<IEnumerable<Product>> FindByProviderIdAsync(ProviderId providerId);
        
    /// <summary>
    /// This method retrieves all products associated with a specific warehouse ID.
    /// </summary>
    /// <returns>
    /// An enumerable collection of Product aggregates that belong to the specified warehouse.
    /// </returns>
    Task<IEnumerable<Inventory>> FindByWarehouseIdAsync(string warehouseId);
    
    /// <summary>
    /// This method checks if a product with the specified ID exists in the database.
    /// </summary>
    Task<bool> ExistsByIdAsync(string productId);
}