using StockSip.Platform.API.InventoryManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Entities;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Queries;

namespace StockSip.Platform.API.InventoryManagement.Domain.Services;

public interface IProductQueryService
{
    Task<IEnumerable<Product>> Handle(GetAllProductsByProviderIdQuery query);
    Task<IEnumerable<Inventory>> Handle(GetAllProductsByWarehouseIdQuery query);
    Task<Product?> Handle(GetProductByIdQuery query);
    Task<IEnumerable<Inventory>> Handle(GetProductsByFullNameAndWarehouseIdQuery query);
    Task<IEnumerable<Inventory>> Handle(GetAllProductsByProfileIdQuery query);
    Task<Inventory?> Handle(GetProductByIdAndWarehouseIdAndExpirationDateQuery query);
}