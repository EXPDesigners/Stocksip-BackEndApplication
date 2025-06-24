using StockSip.Platform.API.InventoryManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Entities;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Queries;

namespace StockSip.Platform.API.InventoryManagement.Domain.Services;

public interface IProductQueryService
{
    Task<IEnumerable<Product>> Handle(GetAllProductsByProviderIdAndWarehouseIdQuery query);
    Task<IEnumerable<Product>> Handle(GetAllProductsByWarehouseIdQuery query);
    Task<Product?> Handle(GetProductByIdQuery query);
    Task<IEnumerable<Product>> Handle(GetProductsByFullNameAndWarehouseIdQuery query);
    Task<IEnumerable<Product>> Handle(GetAllProductsByAccountIdQuery query);
    Task<IEnumerable<ProductExit>> Handle(GetAllProductExitsByProductIdQuery query);
}