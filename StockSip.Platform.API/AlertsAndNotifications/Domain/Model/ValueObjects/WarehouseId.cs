namespace StockSip.Platform.API.AlertsAndNotifications.Domain.Model.ValueObjects;

/// <summary>
/// This record defines the identifier of a warehouse that stores a product that generates alerts and notifications.
/// </summary>
public record WarehouseId()
{
    /// <summary>
    /// The unique identifier for the warehouse.
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// The default constructor for the WarehouseId record.
    /// </summary>
    /// <param name="id">The unique identifier for the warehouse. </param>
    /// <exception cref="ArgumentException">Warehouse ID must be a non-empty string.</exception>
    public WarehouseId(string id) : this()
    {
        if (id == null || id.Trim().Length == 0)
        {
            throw new ArgumentException("Warehouse ID must be a non-empty string.");
        }
        Id = id;
    }
}