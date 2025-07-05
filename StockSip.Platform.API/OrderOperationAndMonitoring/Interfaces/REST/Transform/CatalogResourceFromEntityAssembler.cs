using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.Aggregates;
using StockSip.Platform.API.OrderOperationAndMonitoring.Interfaces.REST.Resources;

namespace StockSip.Platform.API.OrderOperationAndMonitoring.Interfaces.REST.Transform;

public class CatalogResourceFromEntityAssembler
{
    public static CatalogResource ToResourceFromEntity(Catalog entity) =>
        new(
            entity.CatalogId,
            entity.AccountId.Id,
            entity.Name.Value,
            entity.DateCreated.Value,
            entity.IsPublished);
}
