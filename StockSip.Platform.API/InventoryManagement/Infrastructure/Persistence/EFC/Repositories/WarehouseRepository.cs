using Microsoft.EntityFrameworkCore;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Entities;
using StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;
using StockSip.Platform.API.InventoryManagement.Domain.Repositories;
using StockSip.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using StockSip.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace StockSip.Platform.API.InventoryManagement.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// This class implements the IWarehouseRepository interface, providing methods to interact with the Warehouse aggregate.
/// </summary>
/// <param name="context"></param>
public class WarehouseRepository(AppDbContext context) : BaseRepository<Warehouse>(context), IWarehouseRepository
{
    /// <summary>
    /// This method retrieves a list of product exits associated with a specific warehouse ID.
    /// </summary>
    /// <returns>
    /// A list of ProductExit entities that match the specified warehouse ID.
    /// </returns>
    public async Task<IEnumerable<ProductExit>> FindAllProductExitsByWarehouseIdAsync(string warehouseId)
    {
        return await Context.Set<ProductExit>()
            .Where(exit => exit.WarehouseId == warehouseId)
            .ToListAsync();
    }

    /// <summary>
    /// This method retrieves all product exits for a specific product in a specific warehouse.
    /// </summary>
    /// <returns>
    /// A list of ProductExit entities that match the specified product ID and warehouse ID.
    /// </returns>
    public async Task<IEnumerable<ProductExit>> FindAllProductExitsByProductIdAndWarehouseIdAsync(string productId, string warehouseId)
    {
        return await Context.Set<ProductExit>()
            .Where(exit => exit.ProductId == productId && exit.WarehouseId == warehouseId)
            .ToListAsync();
    }

    /// <summary>
    /// This method checks if a warehouse with the specified name and profile ID exists in the database.
    /// </summary>
    /// <returns>
    /// A task that represents the asynchronous operation. 
    /// The task result contains:
    /// <c>true</c> if a warehouse exists with the specified name and profile ID; 
    /// <c>false</c> otherwise.
    /// </returns>
    public async Task<bool> ExistByNameIgnoreCaseAndProfileIdAsync(string name, ProfileId profileId)
    {
        return await Context.Set<Warehouse>()
            .AnyAsync(w => 
                w.Name.ToLower() == name.ToLower() && 
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
    public async Task<bool> ExistsByAddressStreetAndAddressCityAndAddressPostalCodeIgnoreCaseAndProfileIdAsync(string street, string city, string postalCode, ProfileId profileId) 
    {
        return await Context.Set<Warehouse>()
            .AnyAsync(w => 
                w.Address.Street.ToLower() == street.ToLower() &&
                w.Address.City.ToLower() == city.ToLower() &&
                w.Address.PostalCode.ToLower() == postalCode.ToLower() &&
                w.ProfileId == profileId);
    }

    /// <summary>
    /// This method checks if a warehouse with the specified name, profile ID, and a different warehouse ID exists in the database.
    /// </summary>
    /// <returns>True if a warehouse exists with the specified name, profile ID, and a different warehouse ID; otherwise, false.</returns>
    public async Task<bool> ExistsByNameIgnoreCaseAndProfileIdAndWarehouseIdIsNotAsync(string name, ProfileId profileId, string warehouseId)
    {
        return await Context.Set<Warehouse>()
            .AnyAsync(w =>
                w.Name.ToLower() == name.ToLower() &&
                w.ProfileId == profileId &&
                w.WarehouseId != warehouseId);
    }

    /// <summary>
    /// This method checks if a warehouse exists by its address, city, postal code, profile ID, and a different warehouse ID.
    /// </summary>
    /// <returns>True if a warehouse exists with the specified address, city, postal code, profile ID, and a different warehouse ID; otherwise, false.</returns>
    public async Task<bool> ExistsByAddressStreetAndAddressCityAndAddressPostalCodeIgnoreCaseAndProfileIdAndProfileIdIsNotAsync(
            string street, string city, string postalCode, ProfileId profileId, string warehouseId)
    {
        return await Context.Set<Warehouse>()
            .AnyAsync(w =>
                w.Address.Street.ToLower() == street.ToLower() &&
                w.Address.City.ToLower() == city.ToLower() &&
                w.Address.PostalCode.ToLower() == postalCode.ToLower() &&
                w.ProfileId == profileId &&
                w.WarehouseId != warehouseId);
    }
}