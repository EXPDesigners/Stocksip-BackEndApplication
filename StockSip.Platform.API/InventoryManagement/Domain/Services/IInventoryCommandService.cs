using StockSip.Platform.API.InventoryManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;

namespace StockSip.Platform.API.InventoryManagement.Domain.Services;

public interface IInventoryCommandService
{
    Task<Inventory?> Handle(DecreaseStockFromProductCommand command);
    Task<Inventory?> Handle(AddStockToProductCommand command);
    Task<Inventory?> Handle(AddProductsToWarehouseCommand command);
    
    Task Handle(DeleteProductFromWarehouseCommand command);
    Task Handle(MoveProductsToAnotherWarehouseCommand command);
}