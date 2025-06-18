using StockSip.Platform.API.InventoryManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;

namespace StockSip.Platform.API.InventoryManagement.Domain.Services;

public interface IWarehouseCommandService
{
    Task<Warehouse?> Handle(CreateWarehouseCommand command);
    Task<Warehouse?> Handle(UpdateWarehouseCommand command);
}