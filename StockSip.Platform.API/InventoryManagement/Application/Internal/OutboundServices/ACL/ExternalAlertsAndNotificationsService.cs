using StockSip.Platform.API.AlertsAndNotifications.Application.ACL;

namespace StockSip.Platform.API.InventoryManagement.Application.Internal.OutboundServices.ACL;

/// <summary>
/// This service is used to create alerts and notifications in the Alerts and Notifications context.
/// The information will be sent from the Inventory Management context to the Alerts and Notifications context.
/// </summary>
/// <param name="alertsAndNotificationsContextFacade">
/// The facade for the Alerts and Notifications context.
/// </param>
public class ExternalAlertsAndNotificationsService(AlertsAndNotificationsContextFacade alertsAndNotificationsContextFacade)
{
    /// <summary>
    /// The method is used to create an alert in the Alerts and Notifications context.
    /// </summary>
    public void CreateAlert(string title, string message, string severity, string type, string profileId, string productId,
        string warehouseId)
    {
        var alertCreated = alertsAndNotificationsContextFacade.CreateAlert(title, 
            message, severity, type, profileId, productId, warehouseId);
    }
}