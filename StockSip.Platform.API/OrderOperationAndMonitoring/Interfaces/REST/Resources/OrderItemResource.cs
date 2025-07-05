namespace StockSip.Platform.API.OrderOperationAndMonitoring.Interfaces.REST.Resources;

public sealed record OrderItemResource(
    string  Id,
    long    CatalogId,
    string  Name,
    string  ProductType,
    string  Brand,
    int     Content,
    decimal UnitPrice,
    DateTime? DateAdded,
    int     CustomQuantity
);