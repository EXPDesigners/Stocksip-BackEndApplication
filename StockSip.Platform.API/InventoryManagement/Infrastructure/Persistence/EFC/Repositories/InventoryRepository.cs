using Microsoft.EntityFrameworkCore;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;
using StockSip.Platform.API.InventoryManagement.Domain.Repositories;
using StockSip.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using StockSip.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace StockSip.Platform.API.InventoryManagement.Infrastructure.Persistence.EFC.Repositories;

public class InventoryRepository(AppDbContext context) : BaseRepository<Inventory>(context), IInventoryRepository
{
    /// <summary>
    /// This method retrieves an inventory item by the product and warehouse ID.
    /// </summary>
    /// <param name="productId">
    /// The product ID used to retrieve the inventory item.
    /// </param>
    /// <param name="warehouseId">
    /// The warehouse ID used to retrieve the inventory item.
    /// </param>
    /// <returns>
    /// An inventory item.
    /// </returns>
    public async Task<Inventory?> FindByProductIdAndWarehouseIdAsync(string productId, string warehouseId)
    {
        return await Context.Set<Inventory>()
            .FirstOrDefaultAsync(inventory => inventory.ProductId == productId
                                              && inventory.WarehouseId == warehouseId);
    }

    /// <summary>
    /// This async method retrieves an inventory item by its product ID and warehouse ID.
    /// </summary>
    /// <param name="productId">
    /// The ID of the product whose inventory is to be retrieved.
    /// </param>
    /// <param name="warehouseId">
    /// The ID of the warehouse where the product's inventory is located.
    /// </param>
    /// <param name="expirationDate">
    /// The expiration date of the product's inventory.
    /// </param>
    /// <returns>
    /// A task that returns an Inventory object if found, or null if not found.
    /// </returns>
    public async Task<Inventory?> FindByProductIdAndWarehouseIdAndExpirationDateAsync(string productId, string warehouseId, DateTime expirationDate)
    {
        return await Context.Set<Inventory>()
            .FirstOrDefaultAsync(inventory => inventory.ProductId == productId
                                              && inventory.WarehouseId == warehouseId 
                                              && inventory.BestBeforeDate == new ProductBestBeforeDate(expirationDate));
    }

    /// <summary>
    /// This method checks
    /// </summary>
    /// <param name="productId">
    /// The product ID used to check the inventory item.
    /// </param>
    /// <param name="warehouseId">
    /// The warehouse ID used to check the inventory item.
    /// </param>
    /// <returns>
    /// An inventory item.
    /// </returns>
    public async Task<bool> ExistsByProductIdAndWarehouseIdAsync(string productId, string warehouseId)
    {
        return await Context.Set<Inventory>()
            .AnyAsync(inventory => inventory.ProductId == productId);
    }
}