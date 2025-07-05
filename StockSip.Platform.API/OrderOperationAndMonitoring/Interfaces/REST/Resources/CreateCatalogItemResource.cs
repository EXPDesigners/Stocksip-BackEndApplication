namespace StockSip.Platform.API.OrderOperationAndMonitoring.Interfaces.REST.Resources;

public record CreateCatalogItemResource(
    long  CatalogId,
    string  Name,
    string  ProductType,
    string  Brand,
    int     Content,
    decimal UnitPrice);