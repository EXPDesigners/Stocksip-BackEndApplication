namespace StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;

/// <summary>
/// This is a value object that represents the content of a product.
/// </summary>
public record ProductContent
{
    /// <summary>
    /// The content of the product.
    /// </summary>
    public double Content { get; }
    
    /// <summary>
    /// Default constructor that validates the input parameter to ensure that it is a positive number.
    /// </summary>
    /// <param name="content">
    /// The content of the product and the one that will be validated.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Throws this exception if the content is not a positive number.
    /// </exception>
    public ProductContent(double content)
    {
        if (IsValidate(content))
        {
            Content = content;
        }
        else
        {
            throw new ArgumentException("Product content must be a positive number.");
        }
    }
    
    /// <summary>
    /// Method to validate the content input.
    /// </summary>
    /// <param name="content">
    /// The content of the product and the one that will be validated.
    /// </param>
    /// <returns>
    /// Returns true if the content is a positive number. Returns false if it is a negative number or zero.
    /// </returns>
    private static bool IsValidate(double content)
    {
        return !(content < 0);
    }
}