namespace StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;

/// <summary>
/// This is a value object that represents the current stock of a product.
/// </summary>
public record ProductStock()
{
    /// <summary>
    /// The stock of the product.
    /// </summary>
    private int Stock { get; }
    
    /// <summary>
    /// Default constructor for the ProductStock value object.
    /// </summary>
    /// <param name="stock">
    /// The stock of the product and the one that will be validated.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Throws this exception if the stock is not a positive integer number.
    /// </exception>
    public ProductStock(int stock) : this()
    {
        if (IsStockValidate(stock))
        {
            Stock = stock;
        }
        else
        {
            throw new ArgumentException("Product stock must be a positive integer number.");
        }
    }
    
    /// <summary>
    /// Method to validate the stock input.
    /// </summary>
    /// <param name="stock">
    /// The stock of the product and the one that will be validated.
    /// </param>
    /// <returns>
    /// True if the stock is a positive integer number. Returns false if it is a negative number or zero.
    /// </returns>
    private static bool IsStockValidate(int stock)
    {
        return !(stock < 0);
    }
    
    /// <summary>
    /// Method to increase the stock of the product by a specified quantity.
    /// </summary>
    /// <param name="quantity">
    /// The quantity to increase the stock by. Must be a positive integer number.
    /// </param>
    /// <returns>
    /// A new instance of ProductStock with the increased stock value.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Throws this exception if the quantity is not a positive integer number.
    /// </exception>
    public ProductStock IncreaseStock(int quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentException("Increasing stock must be a positive integer number.");
        }

        return new ProductStock(Stock + quantity);
    }
    
    /// <summary>
    /// Method to decrease the stock of the product by a specified quantity.
    /// </summary>
    /// <param name="quantity">
    /// The quantity to decrease the stock by. Must be a positive integer number.
    /// </param>
    /// <returns>
    /// A new instance of ProductStock with the decreased stock value.
    /// </returns>
    /// <exception cref="ArgumentException">
    /// Throws this exception if the quantity is not a positive integer number.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Throws this exception if the stock is insufficient to decrease by the specified quantity.
    /// </exception>
    public ProductStock DecreaseStock(int quantity)
    {
        if (quantity <= 0)
        {
            throw new ArgumentException("Decreasing stock must be a positive integer number.");
        }

        if (Stock < quantity)
        {
            throw new InvalidOperationException("Insufficient stock to decrease by the specified quantity.");
        }

        return new ProductStock(Stock - quantity);
    }
}