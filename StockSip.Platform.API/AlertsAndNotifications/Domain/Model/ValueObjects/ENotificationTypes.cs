namespace StockSip.Platform.API.AlertsAndNotifications.Domain.Model.ValueObjects;

/// <summary>
/// This enum defines the different types of notifications that can be sent within the system.
/// These notifications are used to inform users about various events related to products and purchase orders.
/// The values can be the next ones:
/// - ProductPriceChange: Notification for changes in product prices.
/// - ProductPriceDiscounted: Notification for discounted product prices. Only used for providers and its catalogs.
/// - PurchaseOrderCreated: Notification for the creation of a new purchase order.
/// - PurchaseOrderApproved: Notification for the approval of a purchase order.
/// - PurchaseOrderRejected: Notification for the rejection of a purchase order.
/// - PurchaseOrderUpdated: Notification for updates made to a purchase order.
/// - PurchaseOrderCancelled: Notification for the cancellation of a purchase order.
/// - PurchaseOrderCompleted: Notification for the completion of a purchase order.
/// - SalesOrderReceived: Notification sent to a provider when he receives a sales order.
/// - SalesOrderRejected: Notification sent to a provider when he rejects a sales order.
/// - SalesOrderUpdated: Notification sent to a provider when he updates a sales order.
/// - SalesOrderCompleted: Notification sent to a provider when he completes a sales order.
/// - SalesOrderCancelled: Notification sent to a provider when he cancels a sales order.
/// </summary>
public enum ENotificationTypes
{
    ProductPriceChange,
    ProductPriceDiscounted,
    PurchaseOrderCreated,
    PurchaseOrderApproved,
    PurchaseOrderRejected,
    PurchaseOrderUpdated,
    PurchaseOrderCancelled,
    PurchaseOrderCompleted,
    SalesOrderReceived,
    SalesOrderRejected,
    SalesOrderUpdated,
    SalesOrderCompleted,
    SalesOrderCancelled
}