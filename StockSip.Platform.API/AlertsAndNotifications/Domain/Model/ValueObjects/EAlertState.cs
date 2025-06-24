namespace StockSip.Platform.API.AlertsAndNotifications.Domain.Model.ValueObjects;

/// <summary>
/// This enum represents the different states an alert can be in.
/// The states include:
/// - ACTIVE: The alert is currently active and requires attention.
/// - NOT_READ: The user has not read the alert.
/// - READ: The user has read the alert or notification.
/// - RESOLVED: The alert has been resolved or addressed.
/// </summary>
public enum EAlertState
{
    Active,
    NotRead,
    Read,
    Resolved
}