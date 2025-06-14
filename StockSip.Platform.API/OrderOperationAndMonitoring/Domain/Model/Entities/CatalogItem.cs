using StockSip.Platform.API.Shared.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.Entities;

public class CatalogItem(
    string Id, 
    string name, 
    string brand,
    int content,
    int catalogId,
    string productType,
    Money unitPrice
    )
{
    
}