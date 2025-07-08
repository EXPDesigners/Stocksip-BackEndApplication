namespace StockSip.Platform.API.AlertsAndNotifications.Domain.Model.ValueObjects;

/// <summary>
/// This record defines the identifier of a product that generates alerts and notifications.
/// </summary>
public record ProductId()
{
    /// <summary>
    /// The unique identifier for the product.
    /// </summary>
    public string Id { get; }

    /// <summary>
    /// The default constructor for the ProductId record.
    /// </summary>
    /// <param name="id">The unique identifier for the product. </param>
    /// <exception cref="ArgumentException">Product ID must be a non-empty string.</exception>
    public ProductId(string id) : this()
    {
        if (id == null || id.Trim().Length == 0)
        {
            throw new ArgumentException("Product ID must be a non-empty string.");
        }
        Id = id;
    }
}