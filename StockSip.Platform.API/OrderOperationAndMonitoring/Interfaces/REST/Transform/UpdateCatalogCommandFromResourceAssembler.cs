using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.Commands;
using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.ValueObjects;
using StockSip.Platform.API.OrderOperationAndMonitoring.Interfaces.REST.Resources;

namespace StockSip.Platform.API.OrderOperationAndMonitoring.Interfaces.REST.Transform;

public static class UpdateCatalogCommandFromResourceAssembler
{
    public static UpdateCatalogCommand ToCommandFromResource(long catalogId, UpdateCatalogResource resource) =>
        new(catalogId, resource.AccountId,resource.Name);
}
