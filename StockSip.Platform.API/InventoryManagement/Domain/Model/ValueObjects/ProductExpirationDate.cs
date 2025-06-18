namespace StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;

/// <summary>
/// This is a value object that represents the expiration date of a product.
/// </summary>
public record ProductExpirationDate()
{
    /// <summary>
    /// The expiration date of the product.
    /// </summary>
    private DateTime ExpirationDate { get; }

    /// <summary>
    /// Default constructor for the ProductExpirationDate value object.
    /// </summary>
    /// <param name="expirationDate">
    /// The expiration date of the product, represented as a DateTime value.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Throws this exception if the expiration date is not a future date.
    /// </exception>
    public ProductExpirationDate(DateTime expirationDate) : this()
    {
        if (IsExpirationDateValid(expirationDate))
        {
            ExpirationDate = expirationDate;
        }
        else
        {
            throw new ArgumentException("Expiration date must be a future date.");
        }
    }
    
    /// <summary>
    /// Method to validate the expiration date input.
    /// </summary>
    /// <param name="expirationDate">
    /// The expiration date of the product to be validated.
    /// </param>
    /// <returns>
    /// True if the expiration date is a future date; otherwise, false.
    /// </returns>
    private static bool IsExpirationDateValid(DateTime expirationDate)
    {
        return expirationDate > DateTime.Now;
    }
    
    /// <summary>
    /// Method to get the expiration date of the product.
    /// </summary>
    /// <returns>
    /// The expiration date of the product as a DateTime value.
    /// </returns>
    public DateTime GetExpirationDate()
    {
        return ExpirationDate;
    }
}