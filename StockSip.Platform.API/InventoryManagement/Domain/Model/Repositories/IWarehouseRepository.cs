using StockSip.Platform.API.InventoryManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;
using StockSip.Platform.API.Shared.Domain.Repositories;

namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Repositories;

/// <summary>
/// This interface defines the contract for a repository that manages Warehouse aggregates.
/// </summary>
public interface IWarehouseRepository : IBaseRepository<Warehouse>
{
    /// <summary>
    /// This method checks if a warehouse with the specified name and profile ID exists in the database.
    /// </summary>
    Task<bool> ExistByNameAndProfileIdAsync(string name, int profileId);
    
    /// <summary>
    /// This method checks if a warehouse exists by its address, city, postal code, and profile ID.
    /// </summary>
    Task<bool> ExistsByAddressStreetAndAddressCityAndAddressPostalCodeIgnoreCaseAndProfileId(string address, string city, string postalCode, int profileId);
}