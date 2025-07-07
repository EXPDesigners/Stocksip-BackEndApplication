using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.Commands;
using StockSip.Platform.API.OrderOperationAndMonitoring.Interfaces.REST.Resources;

namespace StockSip.Platform.API.OrderOperationAndMonitoring.Interfaces.REST.Transform;

public class CreateCatalogItemCommandFromResourceAssembler
{
    public static CreateCatalogItemCommand ToCommandFromResource(
        CreateCatalogItemResource r) =>
        new(r.CatalogId,
            r.Name,
            r.ProductType,
            r.Brand,
            r.Content,
            r.UnitPrice);
}