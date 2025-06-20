namespace StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;

/// <summary>
/// This is a value object for the product full name.
/// </summary>
public record ProductName()
{
    public string Name { get; }

    /// <summary>
    /// Constructor for the ProductName value object.
    /// </summary>
    /// <param name="name">The name of the product</param>
    /// <exception cref="ArgumentNullException">The name cannot be null or empty.</exception>
    public ProductName(string name) : this()
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException(nameof(name), "The product name cannot be null or empty.");
        }

        Name = name.Trim();
    }
}