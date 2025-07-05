using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.ValueObjects;
using StockSip.Platform.API.OrderOperationAndMonitoring.Interfaces.REST.Resources;

namespace StockSip.Platform.API.OrderOperationAndMonitoring.Interfaces.REST.Transform;

public class CatalogItemResourceFromEntityAssembler
{
    public static CatalogItemResource ToResourceFromEntity(CatalogItem item) =>
        new(
            item.Id,
            item.Catalog.CatalogId,
            item.Name.Name,
            item.ProductType.Value,
            item.Brand.Value,
            item.Content.Value,
            item.UnitPrice,
            item.DateAdded);
}