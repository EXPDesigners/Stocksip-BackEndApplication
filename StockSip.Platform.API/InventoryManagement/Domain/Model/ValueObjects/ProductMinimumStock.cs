namespace StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;

/// <summary>
/// This is a value object that represents the minimum stock of a product.
/// </summary>
public record ProductMinimumStock()
{
    /// <summary>
    /// The minimum stock of the product.
    /// </summary>
    private int MinimumStock { get; }

    /// <summary>
    /// Default constructor for the ProductMinimumStock value object.
    /// </summary>
    /// <param name="minimumStock">
    /// An integer representing the minimum stock of the product.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Throws this exception if the minimum stock is not a positive number.
    /// </exception>
    public ProductMinimumStock(int minimumStock) : this()
    {
        if (IsMinimumStockValidate(minimumStock))
        {
            throw new ArgumentException("Minimum stock must be a positive number.");
        }
        MinimumStock = minimumStock;
    }
    
    /// <summary>
    /// Method to validate the minimum stock input.
    /// </summary>
    /// <param name="minimumStock">
    /// An integer representing the minimum stock of the product to be validated.
    /// </param>
    /// <returns>
    /// Returns true if the minimum stock is a positive number. Returns false if it is a negative number or zero.
    /// </returns>
    private static bool IsMinimumStockValidate(int minimumStock)
    {
        return !(minimumStock < 0);
    }
    
    /// <summary>
    /// Method to update the minimum stock of the product.
    /// </summary>
    /// <param name="minimumStock">
    /// An integer representing the new minimum stock of the product.
    /// </param>
    /// <returns>
    /// A new instance of ProductMinimumStock with the updated minimum stock value.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Throws this exception if the new minimum stock is not a positive number.
    /// </exception>
    public ProductMinimumStock UpdateMinimumStock(int minimumStock)
    {
        if (IsMinimumStockValidate(minimumStock))
        {
            throw new ArgumentException("New minimum stock must be a positive number.");
        }
        
        return new ProductMinimumStock(minimumStock);
    }
}