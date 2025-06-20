namespace StockSip.Platform.API.AlertsAndNotifications.Domain.Model.Commands;

/// <summary>
/// This command represents a request to mark an alert as resolved.
/// </summary>
/// <param name="AlertId">
/// The unique identifier of the alert to be marked as resolved.
/// </param>
public record MarkAlertAsResolvedCommand(string AlertId);