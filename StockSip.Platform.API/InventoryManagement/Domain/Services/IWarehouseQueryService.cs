using StockSip.Platform.API.InventoryManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Entities;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Queries;

namespace StockSip.Platform.API.InventoryManagement.Domain.Services;

public interface IWarehouseQueryService
{
    Task<Warehouse?> Handle(GetWarehouseByIdQuery query);
    Task<IEnumerable<Warehouse>> Handle(GetAllWarehousesByAccountIdQuery query);
    Task<IEnumerable<Warehouse>> Handle(GetAllWarehousesQuery query);
    
    Task<IEnumerable<ProductExit>> Handle(GetAllProductExitsByWarehouseIdQuery query);
    Task<IEnumerable<ProductExit>> Handle(GetAllProductExitsByProductIdAndWarehouseIdQuery query);
    Task<int?> Handle(GetWarehousesCountUsagesQuery query);
}