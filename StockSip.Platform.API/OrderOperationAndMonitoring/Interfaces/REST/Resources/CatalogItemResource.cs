namespace StockSip.Platform.API.OrderOperationAndMonitoring.Interfaces.REST.Resources;

public record CatalogItemResource(
    string    Id,
    long    CatalogId,
    string    Name,
    string    ProductType,
    string    Brand,
    int       Content,
    decimal   UnitPrice,
    DateTime  DateAdded);