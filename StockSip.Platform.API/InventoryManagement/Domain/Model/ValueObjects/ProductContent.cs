namespace StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;

/// <summary>
/// This is a value object that represents the content of a product.
/// </summary>
public record ProductContent
{
    /// <summary>
    /// Default constructor that validates the input parameter to ensure that it is a positive number.
    /// </summary>
    /// <param name="content">
    /// The content of the product and the one that will be validated.
    /// </param>
    public ProductContent(double content)
    {
        ValidateProductContent(content);
    }
    
    /// <summary>
    /// Method to validate the content input.
    /// </summary>
    /// <param name="content">
    /// The content of the product and the one that will be validated.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Throws this exception if the content is negative or zero.
    /// </exception>
    private static void ValidateProductContent(double content)
    {
        if (content < 0)
        {
            throw new ArgumentException("Product content must be greater than zero.");
        }
    }
}