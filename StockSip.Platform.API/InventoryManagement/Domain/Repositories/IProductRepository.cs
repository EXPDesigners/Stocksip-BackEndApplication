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
    Task<IEnumerable<Product>> FindByProviderIdAndWarehouseIdAsync(ProviderId providerId, string warehouseId);
        
    /// <summary>
    /// This method retrieves all products associated with a specific warehouse ID.
    /// </summary>
    Task<IEnumerable<Product>> FindByWarehouseIdAsync(string warehouseId);

    /// <summary>
    /// This method retrieves a product by its ID, warehouse ID, and expiration date.
    /// </summary>
    Task<Product?> FindByProductIdAndWarehouseIdAndExpirationDateAsync(string productId, string warehouseId, DateTime expirationDate);
    
    /// <summary>
    /// This method retrieves all inventory items that match the specified full name and warehouse ID.
    /// </summary>
    Task<IEnumerable<Product>> FindByFullNameAndWarehouseId(string brandName, string liquorType, string? additionalName, string warehouseId);

    /// <summary>
    /// Method to retrieve all products associated with a specific profile ID.
    /// </summary>
    Task<IEnumerable<Product>> FindProductsByAccountIdAsync(AccountId accountId);

    /// <summary>
    /// Method to retrieve all product exits associated with a specific product ID.
    /// </summary>
    Task<IEnumerable<ProductExit>> FindProductExitsByProductIdAsync(string productId);
    
    /// <summary>
    /// This method checks if a product with the specified ID exists in the database.
    /// </summary>
    Task<bool> ExistsByIdAsync(string productId);

    /// <summary>
    /// This method checks if a product with the specified full name (brand name, liquor type, and additional name) exists in the database, ignoring lower or upper case.
    /// </summary>
    Task<bool> ExistsByFullNameIgnoreCase(string brandName, string liquorType, string? additionalName);
}