namespace StockSip.Platform.API.AlertsAndNotifications.Domain.Model.ValueObjects;

/// <summary>
/// This enum represents the different types of alerts that can be generated in the system.
/// The alerts are related to product stock levels and expiration status.
/// The possible values are:
/// - ProductLowStock: Alert for products that are running low on stock with an alert type of warning.
/// - ProductOutOfStock: Alert for products that are completely out of stock with an alert type of critical.
/// - ProductAboutToExpire: Alert for products that are nearing their expiration date with an alert type of warning.
/// - ProductExpired: Alert for products that have already expired with an alert type of critical.
/// </summary>
public enum EAlertTypes
{
    ProductLowStock,
    ProductOutOfStock,
    ProductAboutToExpire,
    ProductExpired
}