using StockSip.Platform.API.InventoryManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Entities;

namespace StockSip.Platform.API.InventoryManagement.Domain.Services;

public interface IProductCommandService
{
    Task<Product?> Handle(CreateProductCommand command);
    Task<Product?> Handle(UpdateProductCommand command);
    
    Task<Inventory?> Handle(DeleteProductFromWarehouseCommand command);
    Task<Inventory?> Handle(AddProductsToWarehouseCommand command);
    Task<Inventory?> Handle(DecreaseStockFromProductCommand command);
    Task<Inventory?> Handle(AddStockToProductCommand command);
    Task<Inventory?> Handle(MoveProductsToAnotherWarehouseCommand command);
}