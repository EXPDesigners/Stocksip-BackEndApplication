namespace StockSip.Platform.API.AlertsAndNotifications.Interfaces.ACL;

/// <summary>
/// This interface defines the contract for the Alerts and Notifications Context Facade.
/// </summary>
public interface IAlertsAndNotificationsContextFacade
{
    /// <summary>
    /// This method creates a new alert in the system with the specified parameters.
    /// </summary>
    Task<string> CreateAlert(
        string title,
        string message,
        string severity,
        string type,
        string profileId,
        string productId,
        string warehouseId);
}