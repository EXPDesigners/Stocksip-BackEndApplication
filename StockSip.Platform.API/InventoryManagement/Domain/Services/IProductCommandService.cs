using StockSip.Platform.API.InventoryManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Entities;

namespace StockSip.Platform.API.InventoryManagement.Domain.Services;

public interface IProductCommandService
{
    Task<Product?> Handle(CreateProductCommand command);
    Task<Product?> Handle(UpdateProductCommand command);
    Task<Product?> Handle(DeleteProductFromWarehouseCommand command);
    Task<Product?> Handle(AddProductsToWarehouseCommand command);
    
    Task<Inventory?> Handle(DecreaseStockFromProductCommand command);
    Task<Inventory?> Handle(AddStockToProductCommand command);
    
    Task<Product?> Handle(MoveProductsToAnotherWarehouseCommand command);
}