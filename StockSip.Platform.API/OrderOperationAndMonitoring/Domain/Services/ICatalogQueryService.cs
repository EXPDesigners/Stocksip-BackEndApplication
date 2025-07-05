using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.Aggregates;
using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.Queries;
using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Services;

public interface ICatalogQueryService
{
    Task<IEnumerable<Catalog>>     Handle(GetCatalogsByAccountQuery              query);
    Task<IEnumerable<Catalog>>     Handle(GetPublishedCatalogsByAccountIdQuery   query);
    Task<IEnumerable<Catalog>>     Handle(GetPublishedCatalogsQuery              query);
    Task<IEnumerable<Catalog>>     Handle(GetAllCatalogsQuery                    query);

    Task<Catalog?>                 Handle(GetCatalogByIdQuery                    query);

    Task<IEnumerable<CatalogItem>> Handle(GetCatalogItemsByCatalogIdQuery        query);
    
    Task<CatalogItem?>             Handle(GetCatalogItemByIdQuery                query);

    Task<IEnumerable<Catalog>>     GetPublishedCatalogsByProviderEmailAsync(string email);
}