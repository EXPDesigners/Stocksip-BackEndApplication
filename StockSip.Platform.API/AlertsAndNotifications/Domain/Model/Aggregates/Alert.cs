using StockSip.Platform.API.AlertsAndNotifications.Domain.Model.Commands;
using StockSip.Platform.API.AlertsAndNotifications.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.AlertsAndNotifications.Domain.Model.Aggregates;

/// <summary>
/// This class represents an Alert aggregate in the domain model.
/// </summary>
/// <param name="title">
/// The title of the alert.
/// </param>
/// <param name="message">
/// The message of the alert, providing details about the alert.
/// </param>
/// <param name="severity">
/// The severity of the alert, indicating its importance or urgency.
/// </param>
/// <param name="type">
/// The type of the alert, categorizing it into a specific type.
/// </param>
/// <param name="profileId">
/// The unique identifier of the profile associated with the alert.
/// </param>
/// <param name="productId">
/// The unique identifier of the product associated with the alert.
/// </param>
/// <param name="warehouseId">
/// The unique identifier of the warehouse associated with the alert.
/// </param>
public class Alert(
    string title,
    string message,
    string severity,
    string type,
    ProfileId profileId,
    ProductId productId,
    WarehouseId warehouseId)
{
    /// <summary>
    /// The unique identifier of the alert, generated as a new GUID.
    /// </summary>
    public string Id { get; } = Guid.NewGuid().ToString();

    /// <summary>
    /// The title of the alert, providing a brief description of the alert.
    /// </summary>
    public string Title { get; private set; } = title;
    
    /// <summary>
    /// The message of the alert, providing detailed information about the alert.
    /// </summary>
    public string Message { get; private set; } = message;

    /// <summary>
    /// The severity of the alert, indicating its importance or urgency.
    /// </summary>
    public ESeverityTypes Severity { get; private set; } = Enum.Parse<ESeverityTypes>(severity, true); 

    /// <summary>
    /// The type of the alert, categorizing it into a specific type.
    /// </summary>
    public EAlertTypes Type { get; private set; } = Enum.Parse<EAlertTypes>(type, true);

    /// <summary>
    /// The date and time when the alert was created, initialized to the current UTC time.
    /// </summary>
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    /// <summary>
    /// The date and time when the alert was resolved, if applicable.
    /// </summary>
    public DateTime? ResolvedAt { get; private set; }

    /// <summary>
    /// The state of the alert, indicating whether it is active, read, or resolved.
    /// </summary>
    public EAlertState State { get; private set; } = EAlertState.Active;
    
    /// <summary>
    /// The unique identifier of the profile associated with the alert.
    /// </summary>
    public ProfileId ProfileId { get; private set; } = profileId;
    
    /// <summary>
    /// The unique identifier of the product associated with the alert.
    /// </summary>
    public ProductId ProductId { get; private set; } = productId;

    /// <summary>
    /// The unique identifier of the warehouse associated with the alert.
    /// </summary>
    public WarehouseId WarehouseId { get; private set; } = warehouseId;

    /// <summary>
    /// Command handler constructor for the Alert class.
    /// </summary>
    /// <param name="command">
    /// The command that contains the necessary information to create an alert.
    /// </param>
    public Alert(CreateAlertCommand command) : 
        this(command.Title, command.Message, command.Severity, command.Type, 
            command.ProfileId, command.ProductId, command.WarehouseId)
    { }

    /// <summary>
    /// This method marks the alert as read, changing its state to Read.
    /// </summary>
    public void Read()
    {
        State = EAlertState.Read;
    }
    
    /// <summary>
    /// This method resolves the alert, changing its state to Resolved and setting the resolved time to the current UTC time.
    /// </summary>
    public void Resolve()
    {
        State = EAlertState.Resolved;
        ResolvedAt = DateTime.UtcNow;
    }
}