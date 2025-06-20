using StockSip.Platform.API.AlertsAndNotifications.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.AlertsAndNotifications.Domain.Model.Queries;

/// <summary>
/// This record defines a query to retrieve all alerts associated with a specific product ID.
/// </summary>
/// <param name="ProductId">
/// The unique identifier of the product for which alerts are being queried.
/// </param>
public record GetAllAlertsByProductIdQuery(ProductId ProductId);