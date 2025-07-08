using StockSip.Platform.API.InventoryManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Entities;

namespace StockSip.Platform.API.InventoryManagement.Domain.Services;

public interface IProductCommandService
{
    Task<Product?> Handle(CreateProductCommand command);
    Task<Product?> Handle(UpdateProductCommand command);
    Task<Product?> Handle(UpdateProductMinimumStockCommand command);
    Task Handle(DeleteProductCommand command);
}