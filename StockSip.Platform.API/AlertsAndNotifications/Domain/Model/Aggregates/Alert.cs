using StockSip.Platform.API.AlertsAndNotifications.Domain.Model.Commands;
using StockSip.Platform.API.AlertsAndNotifications.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.AlertsAndNotifications.Domain.Model.Aggregates;

/// <summary>
/// This class represents an Alert aggregate in the domain model.
/// </summary>
public class Alert
{
    /// <summary>
    /// The unique identifier of the alert, generated as a new GUID.
    /// </summary>
    public string Id { get; private set; }

    /// <summary>
    /// The title of the alert, providing a brief description of the alert.
    /// </summary>
    public string Title { get; private set; }
    
    /// <summary>
    /// The message of the alert, providing detailed information about the alert.
    /// </summary>
    public string Message { get; private set; }

    /// <summary>
    /// The severity of the alert, indicating its importance or urgency.
    /// </summary>
    public ESeverityTypes Severity { get; private set; }

    /// <summary>
    /// The type of the alert, categorizing it into a specific type.
    /// </summary>
    public EAlertTypes Type { get; private set; }

    /// <summary>
    /// The date and time when the alert was created, initialized to the current UTC time.
    /// </summary>
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    /// The date and time when the alert was resolved, if applicable.
    /// </summary>
    public DateTime? ResolvedAt { get; private set; }

    /// <summary>
    /// The state of the alert, indicating whether it is active, read, or resolved.
    /// </summary>
    public EAlertState State { get; private set; }
    
    /// <summary>
    /// The unique identifier of the profile associated with the alert.
    /// </summary>
    public AccountId AccountId { get; private set; }
    
    /// <summary>
    /// The unique identifier of the product associated with the alert.
    /// </summary>
    public ProductId ProductId { get; private set; }

    /// <summary>
    /// The unique identifier of the warehouse associated with the alert.
    /// </summary>
    public WarehouseId WarehouseId { get; private set; }

    /// <summary>
    /// Parameterless constructor required by Entity Framework for materialization.
    /// </summary>
    public Alert() { }

    /// <summary>
    /// Main constructor for creating a new alert in the domain logic.
    /// </summary>
    /// <param name="title">The title of the alert.</param>
    /// <param name="message">The message of the alert.</param>
    /// <param name="severity">The severity of the alert (as string, will be parsed to enum).</param>
    /// <param name="type">The type of the alert (as string, will be parsed to enum).</param>
    /// <param name="accountId">The account identifier associated with the alert.</param>
    /// <param name="productId">The product identifier associated with the alert.</param>
    /// <param name="warehouseId">The warehouse identifier associated with the alert.</param>
    public Alert(
        string title,
        string message,
        string severity,
        string type,
        AccountId accountId,
        ProductId productId,
        WarehouseId warehouseId)
    {
        Id = Guid.NewGuid().ToString();
        Title = title;
        Message = message;
        Severity = Enum.Parse<ESeverityTypes>(severity, true);
        Type = Enum.Parse<EAlertTypes>(type, true);
        CreatedAt = DateTime.UtcNow;
        State = EAlertState.Active;
        AccountId = accountId;
        ProductId = productId;
        WarehouseId = warehouseId;
    }

    /// <summary>
    /// Command handler constructor for the Alert class.
    /// </summary>
    /// <param name="command">The command that contains the necessary information to create an alert.</param>
    public Alert(CreateAlertCommand command)
        : this(command.Title, command.Message, command.Severity, command.Type, command.AccountId, command.ProductId, command.WarehouseId)
    { }

    /// <summary>
    /// Marks the alert as read, changing its state to Read.
    /// </summary>
    public void Read()
    {
        State = EAlertState.Read;
    }
}