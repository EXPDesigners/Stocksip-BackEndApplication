using StockSip.Platform.API.PaymentAndSubscription.Interfaces.REST.Resources;

namespace StockSip.Platform.API.OrderOperationAndMonitoring.Interfaces.REST.Resources;

public record CreateOrderResource(
    DateTime?            OrderDate,
    AccountResource      Buyer,
    AccountResource      Supplier,
    IList<OrderItemResource> Items,
    decimal              TotalAmount,
    int                  TotalItems
);