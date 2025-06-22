using StockSip.Platform.API.InventoryManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Queries;

namespace StockSip.Platform.API.InventoryManagement.Domain.Services;

public interface IInventoryQueryService
{
    Task<Inventory?> Handle(GetInventoryByIdQuery query);
    Task<Inventory?> Handle(GetInventoriesByProductIdAndWarehouseIdQuery query);
}