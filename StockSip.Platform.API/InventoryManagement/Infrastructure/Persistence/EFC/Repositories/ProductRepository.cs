using Microsoft.EntityFrameworkCore;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Entities;
using StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;
using StockSip.Platform.API.InventoryManagement.Domain.Repositories;
using StockSip.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using StockSip.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace StockSip.Platform.API.InventoryManagement.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// This class implements the IProductRepository interface, providing methods to interact with the Product aggregate.
/// </summary>
public class ProductRepository(AppDbContext context) : BaseRepository<Product>(context), IProductRepository
{
    /// <summary>
    /// This async method retrieves all products associated with a specific provider and warehouse ID.
    /// </summary>
    /// <param name="providerId">
    /// The ID of the provider whose products in a specific warehouse are to be retrieved.
    /// </param>
    /// <param name="warehouseId">
    /// The ID of the warehouse whose products are to be retrieved.
    /// </param>
    /// <returns>
    /// A list of products that belong to the specified provider and warehouse ID.
    /// </returns>
    public async Task<IEnumerable<Product>> FindByProviderIdAndWarehouseIdAsync(ProviderId providerId, string warehouseId)
    {
        return await Context.Set<Product>()
            .Where(product => product.ProviderId == providerId && 
                              product.Inventories.Any(inventory => inventory.WarehouseId == warehouseId))
            .Include(product => product.Inventories.Any(inventory => inventory.WarehouseId == warehouseId))
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
    public async Task<IEnumerable<Product>> FindByWarehouseIdAsync(string warehouseId)
    {
        return await Context.Set<Product>()
            .Where(product => product.Inventories.Any(inventory => inventory.WarehouseId == warehouseId))
            .Include(product => product.Inventories.Where(inventory => inventory.WarehouseId == warehouseId))
            .ToListAsync();
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
    public async Task<Inventory?> FindInventoryByProductIdAndWarehouseIdAndExpirationDateAsync(string productId, string warehouseId,
        DateTime expirationDate)
    {
        return await Context.Set<Inventory>()
            .FirstOrDefaultAsync(inventory => inventory.ProductId == productId
                                              && inventory.WarehouseId == warehouseId 
                                              && inventory.ExpirationDate == new ProductExpirationDate(expirationDate));
    }

    /// <summary>
    /// This async method retrieves a product by its ID, warehouse ID, and expiration date.
    /// </summary>
    /// <param name="productId">
    /// The ID of the product to be retrieved.
    /// </param>
    /// <param name="warehouseId">
    /// The ID of the warehouse where the product is located.
    /// </param>
    /// <param name="expirationDate">
    /// The expiration date of the product's inventory.
    /// </param>
    /// <returns>
    /// The Product and its Inventory object if found, or null if not found.
    /// </returns>
    public async Task<Product?> FindByProductIdAndWarehouseIdAndExpirationDateAsync(string productId,
        string warehouseId, DateTime expirationDate)
    {
        return await Context.Set<Product>()
            .Where(product => product.ProductId == productId)
            .Include(product => product.Inventories
                .Where(inventory =>
                    inventory.WarehouseId == warehouseId &&
                    inventory.ExpirationDate == new ProductExpirationDate(expirationDate)))
            .FirstOrDefaultAsync();
    }

    /// <summary>
    /// This async method retrieves all product items that match the specified full name and warehouse ID.
    /// </summary>
    /// <param name="brandName">
    /// The name of the brand of the product.
    /// </param>
    /// <param name="liquorType">
    /// The type of liquor of the product.
    /// </param>
    /// <param name="additionalName">
    /// The additional name of the product, if any.
    /// </param>
    /// <param name="warehouseId">
    /// The ID of the warehouse where the inventory is located.
    /// </param>
    /// <returns>
    /// A list of Product objects that match the specified criteria with its correspondent Inventory object.
    /// </returns>
    public async Task<IEnumerable<Product>> FindByFullNameAndWarehouseId(string brandName, string liquorType,
        string? additionalName, string warehouseId)
    {
        if (!Enum.TryParse<ELiquorType>(liquorType, true, out var parsedLiquorType))
        {
            return Enumerable.Empty<Product>();
        }
        
        return await Context.Set<Product>()
            .Where(p =>
                (additionalName == null || p.ProductName.Name.ToLower() == additionalName.ToLower()) &&
                p.Brand.ToLower() == brandName.ToLower() &&
                p.LiquorType == parsedLiquorType &&
                p.Inventories.Any(i => i.WarehouseId == warehouseId))
            .Include(p => p.Inventories
                .Where(i => i.WarehouseId == warehouseId))
            .ToListAsync();

    }

    /// <summary>
    /// Async method to retrieve all products associated with a specific profile ID.
    /// </summary>
    /// <param name="profileId">
    /// The ID of the profile whose products are to be retrieved.
    /// </param>
    /// <returns>
    /// The list of products associated with the specified profile ID.
    /// </returns>
    public async Task<IEnumerable<Product>> FindProductsByProfileIdAsync(ProfileId profileId)
    {
        return await Context.Set<Product>()
            .Where(product => product.Inventories.Any(inventory => inventory.Warehouse.ProfileId == profileId))
            .Include(product => product.Inventories
                .Where(inventory => inventory.Warehouse.ProfileId == profileId))
            .ToListAsync();
    }

    /// <summary>
    /// Async method to retrieve all product exits associated with a specific product ID.
    /// </summary>
    /// <param name="productId">
    /// The ID of the product whose exits are to be retrieved.
    /// </param>
    /// <returns>
    /// A list of ProductExit objects associated with the specified product ID.
    /// </returns>
    public async Task<IEnumerable<ProductExit>> FindProductExitsByProductIdAsync(string productId)
    {
        return await Context.Set<ProductExit>()
            .Where(exit => exit.ProductId == productId)
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
        return await Context.Set<Product>().AnyAsync(product => product.ProductId == productId);
    }

    /// <summary>
    /// This async method checks if a product with the specified full name (brand name, liquor type, and additional name) exists in the database, ignoring upper or lower case.
    /// </summary>
    /// <param name="brandName"> The name of the brand. </param>
    /// <param name="liquorType"> The liquor type of the product. </param>
    /// <param name="additionalName"> The additional name of the product. </param>
    /// <returns></returns>
    public async Task<bool> ExistsByFullNameIgnoreCase(string brandName, string liquorType, string? additionalName)
    {
        if (!Enum.TryParse<ELiquorType>(liquorType, true, out var parsedLiquorType))
        {
            return false;
        }

        return await Context.Set<Product>()
            .AnyAsync(p =>
                p.ProductName.Name.ToLower() == additionalName.ToLower() &&
                    p.Brand.ToLower() == brandName.ToLower() &&
                    p.LiquorType == parsedLiquorType); 
    }
    
    
}