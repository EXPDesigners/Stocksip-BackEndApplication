using StockSip.Platform.API.Shared.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.Entities;

public class CatalogItem(
    string id,
    string name,
    string brand,
    int content,
    int catalogId,
    string productType,
    Money unitPrice
)
{
    public string Id { get; } = new(id);
    public string Name { get; } = name;
    public string Brand { get; } = brand;
    public int Content { get; } = content;
    public int CatalogId { get; } = catalogId;
    public string ProductType { get; } = productType;
    public Money UnitPrice { get; } = unitPrice;
}