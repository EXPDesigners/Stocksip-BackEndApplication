using Microsoft.EntityFrameworkCore;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Entities;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Repositories;
using StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;
using StockSip.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using StockSip.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace StockSip.Platform.API.InventoryManagement.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// This class implements the IProductRepository interface, providing methods to interact with the Product aggregate.
/// </summary>
public class ProductRepository(AppDbContext context) : BaseRepository<Product>(context), IProductRepository
{
    /// <summary>
    /// This async method retrieves all products associated with a specific provider ID.
    /// </summary>
    /// <param name="providerId">
    /// The ID of the provider whose products are to be retrieved.
    /// </param>
    /// <returns>
    /// A list of products that belong to the specified provider.
    /// </returns>
    public async Task<IEnumerable<Product>> FindByProviderIdAsync(ProviderId providerId)
    {
        return await Context.Set<Product>()
            .Where(product => product.ProviderId == providerId)
            .ToListAsync();
    }

    /// <summary>
    /// This async method retrieves all products associated with a specific warehouse ID.
    /// </summary>
    /// <param name="warehouseId">
    /// The ID of the warehouse whose products are to be retrieved.
    /// </param>
    /// <returns>
    /// A list of products that belong to the specified warehouse.
    /// </returns>
    public async Task<IEnumerable<Inventory>> FindByWarehouseIdAsync(string warehouseId)
    {
        return await Context.Set<Inventory>()
            .Where(inventory => inventory.WarehouseId == warehouseId)
            .Include(inventory => inventory.Product)
            .ToListAsync();
    }

    /// <summary>
    /// This async method checks if a product with the specified ID exists in the database.
    /// </summary>
    /// <param name="productId">
    /// The ID of the product to check for existence.
    /// </param>
    /// <returns>
    /// True if a product with the specified ID exists; otherwise, false.
    /// </returns>
    public async Task<bool> ExistsByIdAsync(string productId)
    {
        return await Context.Set<Product>().AnyAsync(product => product.Id == productId);
    }
}