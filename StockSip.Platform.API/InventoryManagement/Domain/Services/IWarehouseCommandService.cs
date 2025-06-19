using StockSip.Platform.API.InventoryManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Entities;

namespace StockSip.Platform.API.InventoryManagement.Domain.Services;

public interface IWarehouseCommandService
{
    Task<Warehouse?> Handle(CreateWarehouseCommand command);
    Task<Warehouse?> Handle(UpdateWarehouseCommand command);
    Task<ProductExit?> Handle(RegisterProductExitCommand command);
}