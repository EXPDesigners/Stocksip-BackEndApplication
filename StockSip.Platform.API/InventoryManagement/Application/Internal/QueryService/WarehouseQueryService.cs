using StockSip.Platform.API.InventoryManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Entities;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Queries;
using StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;
using StockSip.Platform.API.InventoryManagement.Domain.Repositories;
using StockSip.Platform.API.InventoryManagement.Domain.Services;

namespace StockSip.Platform.API.InventoryManagement.Application.Internal.QueryService;

/// <summary>
/// This class implements the IWarehouseQueryService interface to handle queries related to warehouses.
/// </summary>
/// <param name="warehouseRepository">The repository for accessing warehouse data.</param>
public class WarehouseQueryService(IWarehouseRepository warehouseRepository) : IWarehouseQueryService
{
    /// <summary>
    /// This method retrieves a warehouse by its ID.
    /// </summary>
    /// <param name="query">The query containing the warehouse ID.</param>
    /// <returns>The warehouse with the specified ID, or null if not found.</returns>
    public async Task<Warehouse?> Handle(GetWarehouseByIdQuery query)
    {
        return await warehouseRepository.FindByIdAsync(query.WarehouseId);
    }

    /// <summary>
    /// This method retrieves all warehouses by a specific account ID.
    /// </summary>
    /// <param name="query"> The query containing the account ID. </param>
    /// <returns>The list of warehouses with the specified ID, or null if not found.</returns>
    public async Task<IEnumerable<Warehouse>> Handle(GetAllWarehousesByAccountIdQuery query)
    {
        return await warehouseRepository.FindAllByAccountIdAsync(query.AccountId);
    }

    /// <summary>
    /// This method retrieves all warehouses.
    /// </summary>
    /// <param name="query">
    /// The query to retrieve all warehouses. This parameter is not used in the current implementation.
    /// </param>
    /// <returns>
    /// The list of all warehouses.
    /// </returns>
    public async Task<IEnumerable<Warehouse>> Handle(GetAllWarehousesQuery query)
    {
        return await warehouseRepository.ListAsync();
    }

    /// <summary>
    /// This method retrieves all product exits for a specific warehouse ID.
    /// </summary>
    /// <param name="query"> The query containing the warehouse ID. </param>
    /// <returns> A list of ProductExit objects related to a specific warehouse ID. </returns>
    public async Task<IEnumerable<ProductExit>> Handle(GetAllProductExitsByWarehouseIdQuery query)
    {
        return await warehouseRepository.FindAllProductExitsByWarehouseIdAsync(query.WarehouseId);
    }

    /// <summary>
    /// This method retrieves all product exits for a specific product ID and warehouse ID.
    /// </summary>
    /// <param name="query"> The query containing the warehouse and product IDs. </param>
    /// <returns> A list of ProductExit objects related to a specific product and warehouse IDs. </returns>
    public async Task<IEnumerable<ProductExit>> Handle(GetAllProductExitsByProductIdAndWarehouseIdQuery query)
    {
        return await warehouseRepository.FindAllProductExitsByProductIdAndWarehouseIdAsync(query.ProductId, query.WarehouseId);
    }

    public async Task<int?> Handle(GetWarehousesCountUsagesQuery query)
    {
        return await warehouseRepository.CountByAccountIdAsync(new AccountId(query.AccountId));
    }
}