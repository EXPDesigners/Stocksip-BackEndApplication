using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.Aggregates;
using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.Commands;
using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Services;

public interface ICatalogCommandService
{
    Task<Catalog>            Handle(CreateCatalogCommand        command);
    Task<Catalog?>           Handle(UpdateCatalogCommand        command);              // update by command (incluye Id)
    Task<Catalog?>           Handle(string catalogId, UpdateCatalogCommand command);   // update con Id explícito
    Task<Catalog?>           Handle(PublishCatalogCommand       command);

    Task<CatalogItem?>       Handle(CreateCatalogItemCommand    command);
    Task<bool>               Handle(DeleteCatalogItemCommand    command);
}