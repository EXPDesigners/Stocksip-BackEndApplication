namespace StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;

/// <summary>
/// This is a value object that represents the expiration date of a product.
/// </summary>
public record ProductBestBeforeDate()
{
    /// <summary>
    /// The expiration date of the product.
    /// </summary>
    public DateOnly BestBeforeDate { get; }

    /// <summary>
    /// Default constructor for the ProductBestBeforeDate value object.
    /// </summary>
    /// <param name="bestBeforeDate">
    /// The best before date of the product, represented as a DateTime value.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Throws this exception if the best before date is not a future date.
    /// </exception>
    public ProductBestBeforeDate(DateOnly bestBeforeDate) : this()
    {
        if (IsBestBeforeDateValid(bestBeforeDate))
        {
            BestBeforeDate = bestBeforeDate;
        }
        else
        {
            throw new ArgumentException("Expiration date must be a future date.");
        }
    }
    
    /// <summary>
    /// Method to validate the best before date input.
    /// </summary>
    /// <param name="bestBeforeDate">
    /// The best before date of the product to be validated.
    /// </param>
    /// <returns>
    /// True if the best before date is a future date; otherwise, false.
    /// </returns>
    private static bool IsBestBeforeDateValid(DateOnly bestBeforeDate)
    {
        return bestBeforeDate > DateOnly.FromDateTime(DateTime.Now);
    }
    
    /// <summary>
    /// Method to get the best before date of the product.
    /// </summary>
    /// <returns>
    /// The best before date of the product as a DateTime value.
    /// </returns>
    public DateOnly GetBestBeforeDate()
    {
        return BestBeforeDate;
    }
}