using StockSip.Platform.API.InventoryManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Queries;
using StockSip.Platform.API.InventoryManagement.Domain.Repositories;
using StockSip.Platform.API.InventoryManagement.Domain.Services;

namespace StockSip.Platform.API.InventoryManagement.Application.Internal.QueryService;

/// <summary>
/// This class implements the IInventoryQueryService interface to handle queries related to inventories.
/// </summary>
/// <param name="inventoryRepository">
/// The repository for accessing to inventory data.
/// </param>
public class InventoryQueryService(IInventoryRepository inventoryRepository) : IInventoryQueryService
{
    /// <summary>
    /// This method is used to retrieve an inventory object by its ID.
    /// </summary>
    /// <param name="query">
    /// The query containing the details.
    /// </param>
    /// <returns>
    /// An inventory object.
    /// </returns>
    public async Task<Inventory?> Handle(GetInventoryByIdQuery query)
    {
        return await inventoryRepository.FindByIdAsync(query.InventoryId);
    }

    /// <summary>
    /// This method is used to retrieve an inventory object by warehouse and product ID.
    /// </summary>
    /// <param name="query">
    /// The query containing the details.
    /// </param>
    /// <returns>
    /// An inventory object.
    /// </returns>
    public async Task<Inventory?> Handle(GetInventoriesByProductIdAndWarehouseIdQuery query)
    {
        return await inventoryRepository.FindByProductIdAndWarehouseIdAsync(query.ProductId, query.WarehouseId);
    }

    /// <summary>
    /// This method is used to retrieve an inventory object by warehouse ID, product ID and best before date.
    /// </summary>
    /// <param name="query">
    /// The query containing the details.
    /// </param>
    /// <returns>
    /// An inventory object.
    /// </returns>
    public async Task<Inventory?> Handle(GetInventoryByProductIdAndWarehouseIdAndBestBeforeDateQuery query)
    {
        return await inventoryRepository.FindByProductIdAndWarehouseIdAndBestBeforeDateAsync(query.ProductId,
            query.WarehouseId, query.BestBeforeDate);
    }

    public async Task<IEnumerable<Inventory>> Handle(GetAllProductsByWarehouseIdQuery query)
    {
        return await inventoryRepository.FindByWarehouseIdAsync(query.WarehouseId);
    }
}