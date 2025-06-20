using StockSip.Platform.API.AlertsAndNotifications.Domain.Model.Commands;
using StockSip.Platform.API.AlertsAndNotifications.Domain.Model.ValueObjects;
using StockSip.Platform.API.AlertsAndNotifications.Domain.Services;
using StockSip.Platform.API.AlertsAndNotifications.Interfaces.ACL;

namespace StockSip.Platform.API.AlertsAndNotifications.Application.ACL;

/// <summary>
/// This class serves as a facade for the Alerts and Notifications context, providing methods to create alerts for other contexts.
/// </summary>
/// <param name="alertCommandService">
/// The command service for handling alert operations.
/// </param>
/// <param name="alertQueryService">
/// The query service for retrieving alert information.
/// </param>
public abstract class AlertsAndNotificationsContextFacade(
    IAlertCommandService alertCommandService,
    IAlertQueryService alertQueryService
    ) : IAlertsAndNotificationsContextFacade
{
    /// <summary>
    /// This method creates an alert with the specified parameters.
    /// It is used to receive information from other contexts and create an alert.
    /// </summary>
    /// <returns>
    /// The ID of the created alert, or an empty string if the alert could not be created.
    /// </returns>
    public async Task<string> CreateAlert(string title, string message, string severity, string type, string profileId, string productId,
        string warehouseId)
    {
        var targetProductId = new ProductId(productId);
        var targetWarehouseId = new WarehouseId(warehouseId);
        var targetProfileId = new ProfileId(profileId);
        
        var createAlertCommand = new CreateAlertCommand(
            title,
            message,
            severity,
            type,
            targetProfileId,
            targetProductId,
            targetWarehouseId
        );
        var alert = await alertCommandService.Handle(createAlertCommand);
        return alert?.Id ?? "";
    }
}