using StockSip.Platform.API.InventoryManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Queries;
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
}