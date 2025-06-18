using StockSip.Platform.API.InventoryManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Queries;

namespace StockSip.Platform.API.InventoryManagement.Domain.Services;

public interface IProductQueryService
{
    Task<IEnumerable<Product>> Handle(GetAllProductsByProviderIdQuery query);
    Task<IEnumerable<Product>> Handle(GetAllProductsByWarehouseIdQuery query);
    Task<Product?> Handle(GetProductByIdQuery query);
}