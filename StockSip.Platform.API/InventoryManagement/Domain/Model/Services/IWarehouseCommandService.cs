using StockSip.Platform.API.InventoryManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;

namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Services;

public interface IWarehouseCommandService
{
    Task<Warehouse?> handle(CreateWarehouseCommand command); 
}