using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.Commands;
using StockSip.Platform.API.OrderOperationAndMonitoring.Interfaces.REST.Resources;

namespace StockSip.Platform.API.OrderOperationAndMonitoring.Interfaces.REST.Transform;

public static class CreateCatalogCommandFromResourceAssembler
{
    public static CreateCatalogCommand ToCommandFromResource(CreateCatalogResource r) =>
        new(r.AccountId, r.Name);
}
