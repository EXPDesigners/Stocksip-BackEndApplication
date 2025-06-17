using StockSip.Platform.API.InventoryManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Queries;

namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Services;

public interface IWarehouseQueryService
{
    Task<Warehouse?> Handle(GetWarehouseByIdQuery query);
}