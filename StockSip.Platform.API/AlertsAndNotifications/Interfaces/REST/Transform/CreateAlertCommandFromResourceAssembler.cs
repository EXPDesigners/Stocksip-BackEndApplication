using StockSip.Platform.API.AlertsAndNotifications.Domain.Model.Commands;
using StockSip.Platform.API.AlertsAndNotifications.Interfaces.REST.Resources;

namespace StockSip.Platform.API.AlertsAndNotifications.Interfaces.REST.Transform;

/// <summary>
/// This static class is responsible for transforming a CreateAlertResource into a CreateAlertCommand.
/// </summary>
public static class CreateAlertCommandFromResourceAssembler
{
    /// <summary>
    /// This method transforms a CreateAlertResource into a CreateAlertCommand.
    /// </summary>
    /// <param name="resource">
    /// The resource containing the details for creating a new alert.
    /// </param>
    /// <returns>
    /// The command that can be used to create a new alert.
    /// </returns>
    public static CreateAlertCommand ToCommandFromResource(CreateAlertResource resource)
    {
        return new CreateAlertCommand(
            resource.Title,
            resource.Message,
            resource.Severity,
            resource.Type,
            resource.ProfileId,
            resource.ProductId,
            resource.WarehouseId);
    }
}