using Microsoft.EntityFrameworkCore;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Repositories;
using StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;
using StockSip.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using StockSip.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace StockSip.Platform.API.InventoryManagement.Infrastructure.Repositories;

/// <summary>
/// This class implements the IWarehouseRepository interface, providing methods to interact with the Warehouse aggregate.
/// </summary>
/// <param name="context"></param>
public class WarehouseRepository(AppDbContext context) : BaseRepository<Warehouse>(context), IWarehouseRepository
{
    /// <summary>
    /// This method checks if a warehouse with the specified name and profile ID exists in the database.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation. 
    /// The task result contains:
    /// <c>true</c> if a warehouse exists with the specified name and profile ID; 
    /// <c>false</c> otherwise.
    /// </returns>
    public async Task<bool> ExistByNameAndProfileIdAsync(string name, ProfileId profileId)
    {
        return await Context.Set<Warehouse>()
            .AnyAsync(w => 
                w.Name.Equals(name, StringComparison.OrdinalIgnoreCase) && 
                w.ProfileId == profileId);
    }

    /// <summary>
    /// This method checks if a warehouse exists by its address, city, postal code, and profile ID.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation.
    /// The task result contains:
    /// <c>true</c> if a warehouse exists with the exact address components and profile ID;
    /// <c>false</c> otherwise.
    /// </returns>
    public async Task<bool> ExistsByAddressStreetAndAddressCityAndAddressPostalCodeIgnoreCaseAndProfileId(string address, string city, string postalCode, ProfileId profileId) 
    {
        return await Context.Set<Warehouse>()
            .AnyAsync(w => 
                w.Address.Street.Equals(address, StringComparison.OrdinalIgnoreCase) && 
                w.Address.City.Equals(city, StringComparison.OrdinalIgnoreCase) && 
                w.Address.PostalCode.Equals(postalCode, StringComparison.OrdinalIgnoreCase) && 
                w.ProfileId == profileId);
    }
}