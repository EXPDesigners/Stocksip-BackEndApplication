using StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;
using StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;
using StockSip.Platform.API.Shared.Domain.Model.ValueObjects;
using DateTime = System.DateTime;

namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Aggregates;

/// <summary>
/// Represents a product in the inventory management system.
/// </summary>
public partial class Product
{
    /// <summary>
    /// The unique identifier of the product.
    /// </summary>
    public string Id { get; } = Guid.NewGuid().ToString();
    
    /// <summary>
    /// The name of the product, which includes the brand name, liquor type, and additional name.
    /// </summary>
    public ProductName ProductName { get; private set; }
    
    /// <summary>
    /// The unit price of the product, represented as a Money value object.
    /// </summary>
    private Money UnitPrice { get; set; }
    
    /// <summary>
    /// The brand associated with the product, represented as a Brand entity.
    /// </summary>
    public string Brand { get; internal set; }
    
    /// <summary>
    /// The type of liquor represented by the product, defined as an enumeration.
    /// </summary>
    public ELiquorType LiquorType { get; private set; }
    
    /// <summary>
    /// The minimum stock level for the product, represented as a ProductMinimumStock value object.
    /// </summary>
    public ProductMinimumStock MinimumStock { get; private set; }
    
    /// <summary>
    /// The URL of the product's image, represented as an ImageUrl value object.
    /// </summary>
    public ImageUrl ImageUrl { get; private set; }
    
    /// <summary>
    /// The unique identifier of the provider associated with the product, if any.
    /// </summary>
    public ProviderId? ProviderId { get; private set; }

    /// <summary>
    /// Default constructor for the Product class.
    /// </summary>
    /// <param name="imageUrl">
    /// The URL of the product's image.
    /// </param>
    /// <param name="additionalName">
    /// The additional name of the product, which can be null or empty.
    /// </param>
    /// <param name="brandName">
    /// The name of the brand associated with the product, represented as a string.
    /// </param>
    /// <param name="liquorType">
    /// The type of liquor represented by the product, defined as a string.
    /// </param>
    /// <param name="unitPriceAmount">
    /// The unit price of the product, represented as an integer amount.
    /// </param>
    /// <param name="minimumStock">
    /// The minimum stock level for the product, represented as an integer.
    /// </param> 
    /// <param name="providerId">
    /// The unique identifier of the provider associated with the product, if any.
    /// </param>
    public Product(string imageUrl, 
                    string? additionalName, 
                    string brandName, 
                    string liquorType, 
                    int unitPriceAmount,
                    int minimumStock,
                    string? providerId = null)
    {
        ProductName = new ProductName(brandName, 
            Enum.Parse<ELiquorType>(liquorType, true), 
                        additionalName);
        LiquorType = Enum.Parse<ELiquorType>(liquorType, true); ;
        Brand = brandName;
        UnitPrice = new Money(unitPriceAmount, new Currency("PEN"));
        MinimumStock = new ProductMinimumStock(minimumStock);
        ImageUrl = new ImageUrl(imageUrl);
        if (providerId != null) ProviderId = new ProviderId(providerId);
    }

    public Product(CreateProductCommand command)
    {
        ProductName = new ProductName(command.BrandName,
                Enum.Parse<ELiquorType>(command.LiquorType, true),
                            command.AdditionalName);
        LiquorType = Enum.Parse<ELiquorType>(command.LiquorType, true);
        Brand = command.BrandName;
        UnitPrice = new Money(command.UnitPriceAmount, new Currency("PEN"));
        MinimumStock = new ProductMinimumStock(command.MinimumStock);
        ImageUrl = new ImageUrl(command.ImageUrl);
        ProviderId = command.ProviderId;
    }

    /// <summary>
    /// Method to set the minimum stock level for the product.
    /// </summary>
    /// <param name="newMinimumStock">
    /// The new minimum stock level to be set, represented as an integer.
    /// </param>
    private void SetMinimumStock(int newMinimumStock)
    {
        MinimumStock = MinimumStock.UpdateMinimumStock(newMinimumStock);
    }
    
    /// <summary>
    /// Retrieves the current minimum stock level of the product.
    /// </summary>
    /// <returns>
    /// Returns the current minimum stock level as a ProductMinimumStock value object.
    /// </returns>
    public ProductMinimumStock GetMinimumStock()
    {
        return MinimumStock;
    }

    /// <summary>
    /// Method to update the product's information, including price, minimum stock, and image URL.
    /// </summary>
    /// <param name="updatedPrice">
    /// The updated price of the product, represented as a double.
    /// </param>
    /// <param name="updatedMinimumStock">
    /// The updated minimum stock level for the product, represented as an integer.
    /// </param>
    /// <param name="updatedImageUrl">
    /// The updated URL of the product's image, represented as a string.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown when the updated price is less than or equal to zero.
    /// </exception>
    public void UpdateInformation(double updatedPrice, int updatedMinimumStock, string updatedImageUrl)
    {
        if (updatedPrice <= 0)
        {
            throw new ArgumentException("Price must be greater than zero: ", nameof(updatedPrice));
        } 
        
        SetMinimumStock(updatedMinimumStock);
        ImageUrl = new ImageUrl(updatedImageUrl);
        UnitPrice = new Money(updatedPrice, UnitPrice.Currency);
    }
}