using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.Commands;

namespace StockSip.Platform.API.OrderOperationAndMonitoring.Interfaces.REST.Transform;

public class DeleteCatalogItemCommandFromIdAssembler
{
    public static DeleteCatalogItemCommand ToCommandFromResource(string itemId) =>
        new(itemId);
}