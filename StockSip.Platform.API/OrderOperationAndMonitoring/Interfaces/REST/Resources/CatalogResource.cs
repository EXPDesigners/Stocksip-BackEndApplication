namespace StockSip.Platform.API.OrderOperationAndMonitoring.Interfaces.REST.Resources;

public record CatalogResource(
    long    catalogId,
    string    AccountId,
    string    Name,
    DateTime  DateCreated,
    bool      IsPublished);