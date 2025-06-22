using StockSip.Platform.API.InventoryManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.Shared.Domain.Repositories;

namespace StockSip.Platform.API.InventoryManagement.Domain.Repositories;

/// <summary>
/// This interface defines the contract for a repository that manages Inventory aggregates.
/// </summary>
public interface IInventoryRepository : IBaseRepository<Inventory>
{
    /// <summary>
    /// This method retrieves an inventory item by the product and warehouse ID.
    /// </summary>
    Task<Inventory?> FindByProductIdAndWarehouseIdAsync(string productId, string warehouseId);
    
    /// <summary>
    /// This method retrieves an inventory item by the product ID, warehouse ID and expiration date.
    /// </summary>
    Task<Inventory?> FindByProductIdAndWarehouseIdAndExpirationDateAsync(string productId, string warehouseId, DateTime expirationDate);

    /// <summary>
    /// This method checks if an inventory exists by the given product and warehouse ID.
    /// </summary>
    Task<bool> ExistsByProductIdAndWarehouseIdAsync(string productId, string warehouseId);
}