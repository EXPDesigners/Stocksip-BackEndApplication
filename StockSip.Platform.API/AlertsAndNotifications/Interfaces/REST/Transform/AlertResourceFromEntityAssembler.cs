using Microsoft.OpenApi.Extensions;
using StockSip.Platform.API.AlertsAndNotifications.Domain.Model.Aggregates;
using StockSip.Platform.API.AlertsAndNotifications.Interfaces.REST.Resources;

namespace StockSip.Platform.API.AlertsAndNotifications.Interfaces.REST.Transform;

/// <summary>
/// This class is responsible for transforming an Alert entity into an AlertResource.
/// </summary>
public static class AlertResourceFromEntityAssembler
{
    /// <summary>
    /// Method to transform an Alert entity into an AlertResource.
    /// </summary>
    /// <param name="entity">
    /// The Alert entity to be transformed into an AlertResource.
    /// </param>
    /// <returns>
    /// The AlertResource that contains the details of the alert.
    /// </returns>
    public static AlertResource ToResourceFromEntity(Alert entity)
    {
        return new AlertResource(
            entity.Id,
            entity.Title,
            entity.Message,
            entity.Severity.GetDisplayName(),
            entity.Type.GetDisplayName(),
            entity.ProductId.Id,
            entity.WarehouseId.Id);
    }
}