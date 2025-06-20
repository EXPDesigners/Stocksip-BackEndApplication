using StockSip.Platform.API.AlertsAndNotifications.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.AlertsAndNotifications.Interfaces.REST.Resources;

/// <summary>
/// This record defines the resource for creating a new alert.
/// </summary>
public record CreateAlertResource(
    string Title,
    string Message,
    string Severity,
    string Type,
    ProfileId ProfileId,
    ProductId ProductId,
    WarehouseId WarehouseId);