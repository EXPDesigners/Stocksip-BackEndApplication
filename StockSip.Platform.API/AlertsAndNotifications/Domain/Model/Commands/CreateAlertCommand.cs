using StockSip.Platform.API.AlertsAndNotifications.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.AlertsAndNotifications.Domain.Model.Commands;

/// <summary>
/// This command represents the creation of a new alert in the system.
/// </summary>
/// <param name="Title">
/// The title of the alert, providing a brief description of the alert.
/// </param>
/// <param name="Message">
/// The message of the alert, providing detailed information about the alert.
/// </param>
/// <param name="Severity">
/// The severity of the alert, indicating its importance or urgency.
/// </param>
/// <param name="Type">
/// The type of the alert, categorizing it into a specific type.
/// </param>
/// <param name="AccountId">
/// The unique identifier of the profile associated with the alert.
/// </param>
/// <param name="ProductId">
/// The unique identifier of the product associated with the alert.
/// </param>
/// <param name="WarehouseId">
/// The unique identifier of the warehouse associated with the alert.
/// </param>
public record CreateAlertCommand(
    string Title,
    string Message,
    string Severity,
    string Type,
    AccountId AccountId,
    ProductId ProductId,
    WarehouseId WarehouseId);