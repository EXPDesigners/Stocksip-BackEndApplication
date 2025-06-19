using StockSip.Platform.API.InventoryManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.Shared.Domain.Repositories;

namespace StockSip.Platform.API.InventoryManagement.Domain.Repositories;

/// <summary>
/// This interface defines the contract for a repository that manages Warehouse aggregates.
/// </summary>
public interface IWarehouseRepository : IBaseRepository<Warehouse>
{
    /// <summary>
    /// This method checks if a warehouse with the specified name and profile ID exists in the database.
    /// </summary>
    /// <returns>True if a warehouse exists with the specified name and profile ID; otherwise, false.</returns>
    Task<bool> ExistByNameIgnoreCaseAndProfileIdAsync(string name, ProfileId profileId);
    
    /// <summary>
    /// This method checks if a warehouse exists by its address, city, postal code, and profile ID.
    /// </summary>
    /// <returns>True if a warehouse exists with the exact address components and profile ID; otherwise, false.</returns>
    Task<bool> ExistsByAddressStreetAndAddressCityAndAddressPostalCodeIgnoreCaseAndProfileIdAsync(string street, string city, string postalCode, ProfileId profileId);

    /// <summary>
    /// This method checks if a warehouse with the specified name, profile ID, and a different warehouse ID exists in the database.
    /// </summary>
    /// <returns>True if a warehouse exists with the specified name, profile ID, and a different warehouse ID; otherwise, false.</returns>
    Task<bool> ExistsByNameIgnoreCaseAndProfileIdAndWarehouseIdIsNotAsync(string name, ProfileId profileId, string warehouseId);
    
    /// <summary>
    /// This method checks if a warehouse exists by its address, city, postal code, profile ID, and a different warehouse ID.
    /// </summary>
    /// <returns>Exists by address, city, postal code, profile ID, and a different warehouse ID; otherwise, false.</returns>
    Task<bool> ExistsByAddressStreetAndAddressCityAndAddressPostalCodeIgnoreCaseAndProfileIdAndProfileIdIsNotAsync(string street, string city, string postalCode, ProfileId profileId, string warehouseId);
    
}