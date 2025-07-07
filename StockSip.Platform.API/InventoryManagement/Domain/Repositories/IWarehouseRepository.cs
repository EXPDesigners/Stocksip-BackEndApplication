using StockSip.Platform.API.InventoryManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Entities;
using StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;
using StockSip.Platform.API.Shared.Domain.Repositories;

namespace StockSip.Platform.API.InventoryManagement.Domain.Repositories;

/// <summary>
/// This interface defines the contract for a repository that manages Warehouse aggregates.
/// </summary>
public interface IWarehouseRepository : IBaseRepository<Warehouse>
{
    /// <summary>
    /// This method retrieves all the warehouses associated with a specific account ID.
    /// </summary>
    /// <returns> A list of warehouse objects </returns>
    Task<IEnumerable<Warehouse>> FindAllByAccountIdAsync(AccountId accountId);
    
    /// <summary>
    /// This method retrieves all product exits associated with a specific warehouse ID.
    /// </summary>
    /// <returns> A list of product exit objects related to a specific warehouse ID. </returns>
    Task<IEnumerable<ProductExit>> FindAllProductExitsByWarehouseIdAsync(string warehouseId);
    
    /// <summary>
    /// This method retrieves all product exits associated with a specific product ID and warehouse ID.
    /// </summary>
    /// <returns> A list of product exit objects related to a specific product ID and warehouse ID. </returns>
    Task<IEnumerable<ProductExit>> FindAllProductExitsByProductIdAndWarehouseIdAsync(string productId, string warehouseId);
    
    /// <summary>
    /// This method checks if a warehouse with the specified name and profile ID exists in the database.
    /// </summary>
    /// <returns>True if a warehouse exists with the specified name and profile ID; otherwise, false.</returns>
    Task<bool> ExistByNameIgnoreCaseAndProfileIdAsync(string name, AccountId accountId);
    
    /// <summary>
    /// This method checks if a warehouse exists by its address, city, postal code, and profile ID.
    /// </summary>
    /// <returns>True if a warehouse exists with the exact address components and profile ID; otherwise, false.</returns>
    Task<bool> ExistsByAddressStreetAndAddressCityAndAddressPostalCodeIgnoreCaseAndProfileIdAsync(string street, string city, string postalCode, AccountId accountId);

    /// <summary>
    /// This method checks if a warehouse with the specified name, profile ID, and a different warehouse ID exists in the database.
    /// </summary>
    /// <returns>True if a warehouse exists with the specified name, profile ID, and a different warehouse ID; otherwise, false.</returns>
    Task<bool> ExistsByNameIgnoreCaseAndProfileIdAndWarehouseIdIsNotAsync(string name, AccountId accountId, string warehouseId);
    
    /// <summary>
    /// This method checks if a warehouse exists by its address, city, postal code, profile ID, and a different warehouse ID.
    /// </summary>
    /// <returns>Exists by address, city, postal code, profile ID, and a different warehouse ID; otherwise, false.</returns>
    Task<bool> ExistsByAddressStreetAndAddressCityAndAddressPostalCodeIgnoreCaseAndProfileIdAndProfileIdIsNotAsync(string street, string city, string postalCode, AccountId accountId, string warehouseId);
    
    /// <summary>
    /// Get the account ID associated with a specific warehouse ID.
    /// </summary>
    /// <param name="warehouseId">The unique identifier of the warehouse.</param>
    /// <returns>The account ID associated with the warehouse.</returns>
    Task<string> FindAccountIdByWarehouseIdAsync(string warehouseId);
    
    /// <summary>
    /// Get the image URL associated with a specific warehouse ID.
    /// </summary>
    /// <param name="warehouseId">The unique identifier of the warehouse.</param>
    /// <returns>The image URL associated with the warehouse.</returns>
    Task<string> FindImageUrlByWarehouseIdAsync(string warehouseId);
    
    /// <summary>
    /// This method counts the number of warehouses associated with a specific account ID.
    /// </summary>
    /// <param name="accountId">The unique identifier of the account.</param>
    /// <returns>A task that represents the asynchronous operation, containing the count of warehouses.</returns>
    Task<int> CountByAccountIdAsync(AccountId accountId);
}