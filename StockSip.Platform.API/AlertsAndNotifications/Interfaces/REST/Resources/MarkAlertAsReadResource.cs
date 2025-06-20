namespace StockSip.Platform.API.AlertsAndNotifications.Interfaces.REST.Resources;

/// <summary>
/// This record defines the resource for marking an alert as read.
/// </summary>
public record MarkAlertAsReadResource(string AlertId);