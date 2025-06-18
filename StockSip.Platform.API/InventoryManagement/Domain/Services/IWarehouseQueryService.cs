using StockSip.Platform.API.InventoryManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Queries;

namespace StockSip.Platform.API.InventoryManagement.Domain.Services;

/// <summary>
/// This interface defines the contract for a service that handles queries related to warehouses.
/// </summary>
public interface IWarehouseQueryService
{
    Task<Warehouse?> Handle(GetWarehouseByIdQuery query);
    
    Task<IEnumerable<Warehouse>> Handle(GetAllWarehousesQuery query);
}