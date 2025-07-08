using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.Queries;

public record GetPublishedCatalogsByAccountIdQuery(AccountId AccountId, bool OnlyPublished);
