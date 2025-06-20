namespace StockSip.Platform.API.AlertsAndNotifications.Domain.Model.ValueObjects;

/// <summary>
/// This enum represents the severity types for an alert.
/// The possible values are:
/// - CRITICAL: Indicates a critical alert that requires immediate attention.
/// - WARNING: Indicates a warning alert that should be addressed but is not immediately critical.
/// - INFO: Indicates an informational alert that does not require immediate action but is useful to know. If the alert is a type of this, then it transforms itself into a notification.
/// </summary>
public enum ESeverityTypes
{
    Critical,
    Warning,
    Info
}