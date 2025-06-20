namespace StockSip.Platform.API.AlertsAndNotifications.Domain.Model.Queries;

/// <summary>
/// This query is used to retrieve an alert by its unique identifier.
/// </summary>
/// <param name="AlertId">
/// The unique identifier of the alert to be retrieved.
/// </param>
public record GetAlertByIdQuery(string AlertId);