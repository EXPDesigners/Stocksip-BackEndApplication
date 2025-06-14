using StockSip.Platform.API.Shared.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.Entities;

public class SalesOrderItem(
    int id,
    int salesOrderId,
    string catalogItemId,
    int quantity,
    string productId,
    Money unitPrice,
    string status
)
{
    /// <summary>
    /// The unique identifier for the sales order item.
    /// </summary>
    public int Id { get; } = id;
    
    /// <summary>
    /// The unique identifier for the sales order to which this item belongs.
    /// </summary>
    public int SalesOrderId { get; } = salesOrderId;
    
    /// <summary>
    /// The unique identifier for the catalog item associated with this sales order item.
    /// </summary>
    public string CatalogItemId { get; } = catalogItemId;
    
    /// <summary>
    /// The quantity of the catalog item in this sales order item.
    /// </summary>
    public int Quantity { get; } = quantity;
    
    /// <summary>
    /// The unique identifier for the product associated with this sales order item.
    /// </summary>
    public string ProductId { get; } = productId;
    
    /// <summary>
    /// The unit price of the catalog item in this sales order item.
    /// </summary>
    public Money UnitPrice { get; } = unitPrice;
    
    /// <summary>
    /// The status of the sales order item, indicating its current state in the sales process.
    /// </summary>
    public string Status { get; } = status;
}
