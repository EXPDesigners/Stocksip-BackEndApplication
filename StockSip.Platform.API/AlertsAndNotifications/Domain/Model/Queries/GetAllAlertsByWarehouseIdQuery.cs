using StockSip.Platform.API.AlertsAndNotifications.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.AlertsAndNotifications.Domain.Model.Queries;

/// <summary>
/// This record defines a query to retrieve all alerts associated with a specific warehouse ID.
/// </summary>
/// <param name="WarehouseId">
/// The unique identifier of the warehouse for which alerts are being requested.
/// </param>
public record GetAllAlertsByWarehouseIdQuery(WarehouseId WarehouseId);