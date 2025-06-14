namespace StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;

/// <summary>
/// This is a value object for the product full name.
/// </summary>
public record ProductName()
{
    /// <summary>
    /// The full name of the product.
    /// </summary>
    public string? FullName { get; }

    /// <summary>
    /// Default constructor that initializes a new ProductName instance.
    /// </summary>
    /// <param name="brandName">
    /// The brand name of the product.
    /// </param>
    /// <param name="productType">
    /// The liquor type of the product.
    /// </param>
    /// <param name="name">
    /// The additional name of the product. It can be null.
    /// </param>
    public ProductName(EBrandName brandName, ELiquorType productType, string? name) : this()
    {
        FullName = string.Join(" ", 
            brandName.ToString(),
            productType.ToString(),
            string.IsNullOrWhiteSpace(name) ? null : name.Trim()
            ).Trim();
    }
}