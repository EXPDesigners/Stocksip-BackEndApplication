using StockSip.Platform.API.PaymentAndSubscription.Interfaces.REST.Resources;

namespace StockSip.Platform.API.OrderOperationAndMonitoring.Interfaces.REST.Resources;

public sealed record PurchaseOrderResource(
    long                 Id,
    DateTime             Date,
    string               Status,
    AccountResource      Buyer,
    AccountResource      Supplier,
    IList<OrderItemResource> Items,
    decimal              TotalAmount,
    int                  TotalItems
);