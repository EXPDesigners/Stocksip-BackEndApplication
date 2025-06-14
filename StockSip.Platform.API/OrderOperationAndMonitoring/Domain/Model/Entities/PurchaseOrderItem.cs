using StockSip.Platform.API.Shared.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.Entities;

public class PurchaseOrderItem(
    int id,
    int purchaseOrderId,
    string catalogItemId,
    int quantity,
    Money unitPrice
)
{
    public int Id { get; } = id;
    public int PurchaseOrderId { get; } = purchaseOrderId;
    public string CatalogItemId { get; } = catalogItemId;
    public int Quantity { get; } = quantity;
    public Money UnitPrice { get; } = unitPrice;
}