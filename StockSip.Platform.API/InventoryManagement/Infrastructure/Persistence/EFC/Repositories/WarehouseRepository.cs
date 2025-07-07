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
    /// This method retrieves all the warehouses associated with a specific account ID.
    /// </summary>
    /// <param name="accountId"> The unique identifier of the account owner of the warehouses.</param>
    /// <returns> A list of warehouse objects. </returns>
    public async Task<IEnumerable<Warehouse>> FindAllByAccountIdAsync(AccountId accountId)
    {
        return await Context.Set<Warehouse>().Where(w => w.AccountId == accountId).ToListAsync();
    }

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
    public async Task<bool> ExistByNameIgnoreCaseAndProfileIdAsync(string name, AccountId accountId)
    {
        return await Context.Set<Warehouse>()
            .AnyAsync(w => 
                w.Name.ToLower() == name.ToLower() && 
                w.AccountId == accountId);
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
    public async Task<bool> ExistsByAddressStreetAndAddressCityAndAddressPostalCodeIgnoreCaseAndProfileIdAsync(string street, string city, string postalCode, AccountId accountId) 
    {
        return await Context.Set<Warehouse>()
            .AnyAsync(w => 
                w.Address.Street.ToLower() == street.ToLower() &&
                w.Address.City.ToLower() == city.ToLower() &&
                w.Address.PostalCode.ToLower() == postalCode.ToLower() &&
                w.AccountId == accountId);
    }

    /// <summary>
    /// This method checks if a warehouse with the specified name, profile ID, and a different warehouse ID exists in the database.
    /// </summary>
    /// <returns>True if a warehouse exists with the specified name, profile ID, and a different warehouse ID; otherwise, false.</returns>
    public async Task<bool> ExistsByNameIgnoreCaseAndProfileIdAndWarehouseIdIsNotAsync(string name, AccountId accountId, string warehouseId)
    {
        return await Context.Set<Warehouse>()
            .AnyAsync(w =>
                w.Name.ToLower() == name.ToLower() &&
                w.AccountId == accountId &&
                w.WarehouseId != warehouseId);
    }

    /// <summary>
    /// This method checks if a warehouse exists by its address, city, postal code, profile ID, and a different warehouse ID.
    /// </summary>
    /// <returns>True if a warehouse exists with the specified address, city, postal code, profile ID, and a different warehouse ID; otherwise, false.</returns>
    public async Task<bool> ExistsByAddressStreetAndAddressCityAndAddressPostalCodeIgnoreCaseAndProfileIdAndProfileIdIsNotAsync(
            string street, string city, string postalCode, AccountId accountId, string warehouseId)
    {
        return await Context.Set<Warehouse>()
            .AnyAsync(w =>
                w.Address.Street.ToLower() == street.ToLower() &&
                w.Address.City.ToLower() == city.ToLower() &&
                w.Address.PostalCode.ToLower() == postalCode.ToLower() &&
                w.AccountId == accountId &&
                w.WarehouseId != warehouseId);
    }

    /// <summary>
    /// This method retrieves the account ID associated with a specific warehouse ID.
    /// </summary>
    /// <param name="warehouseId">The unique identifier of the warehouse.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the account ID associated with the warehouse.</returns>
    /// <exception cref="InvalidOperationException">An exception is thrown if the warehouse is not found.</exception>
    public async Task<string> FindAccountIdByWarehouseIdAsync(string warehouseId)
    {
        var accountId = await Context.Set<Warehouse>()
            .Where(w => w.WarehouseId == warehouseId)
            .Select(w => w.AccountId.Id)
            .FirstOrDefaultAsync();
        
        return accountId ?? throw new InvalidOperationException("Warehouse not found");
    }

    /// <summary>
    /// This method retrieves the image URL of a warehouse by its warehouse ID.
    /// </summary>
    /// <param name="warehouseId">The unique identifier of the warehouse.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the image URL of the warehouse.</returns>
    /// <exception cref="InvalidOperationException">An exception is thrown if the warehouse is not found or if the image URL is null.</exception>
    public async Task<string> FindImageUrlByWarehouseIdAsync(string warehouseId)
    {
        var imageUrl = await Context.Set<Warehouse>()
            .Where(w => w.WarehouseId == warehouseId)
            .Select(w => w.ImageUrl!.ImageUri.ToString())
            .FirstOrDefaultAsync();

        return imageUrl ?? throw new InvalidOperationException("Warehouse not found or image URL is null");
    }

    public async Task<int> CountByAccountIdAsync(AccountId accountId)
    {
        return await Context.Set<Warehouse>()
            .CountAsync(w => w.AccountId == accountId);
    }
}