using StockSip.Platform.API.InventoryManagement.Domain.Model.Entities;
using StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;
using StockSip.Platform.API.Shared.Domain.Model.ValueObjects;
using Object = Mysqlx.Expr.Object;

namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Aggregates;

/// <summary>
/// Represents a product in the inventory management system.
/// </summary>
public partial class Product
{
    /// <summary>
    /// The unique identifier of the warehouse where the product is stored.
    /// </summary>
    public string WarehouseId { get; private set; }
    
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
    public Money UnitPrice { get; private set; }
    
    /// <summary>
    /// The state of the product, which can be either WithStock or OutOfStock.
    /// </summary>
    public EProductState ProductState { get; private set; } = EProductState.WithStock;
    
    /// <summary>
    /// The brand associated with the product, represented as a Brand entity.
    /// </summary>
    public Brand Brand { get; internal set; }
    
    /// <summary>
    /// The unique identifier of the brand associated with the product.
    /// </summary>
    public string BrandId { get; private set; }
    
    /// <summary>
    /// The type of liquor represented by the product, defined as an enumeration.
    /// </summary>
    public ELiquorType LiquorType { get; private set; }
    
    /// <summary>
    /// The current stock of the product, represented as a ProductStock value object.
    /// </summary>
    public ProductStock CurrentStock { get; private set; }
    
    /// <summary>
    /// The minimum stock level for the product, represented as a ProductMinimumStock value object.
    /// </summary>
    public ProductMinimumStock MinimumStock { get; private set; } = new ProductMinimumStock(1);
    
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
    /// <param name="currentStock">
    /// The current stock level of the product, represented as an integer.
    /// </param>
    /// <param name="unitPriceAmount">
    /// The unit price of the product, represented as an integer amount.
    /// </param>
    /// <param name="brandId">
    /// The unique identifier of the brand associated with the product.
    /// </param>
    /// <param name="warehouseId">
    /// The unique identifier of the warehouse where the product is stored.
    /// </param>
    /// <param name="providerId">
    /// The unique identifier of the provider associated with the product, if any.
    /// </param>
    public Product(string imageUrl, 
                    string? additionalName, 
                    string brandName, 
                    string liquorType, 
                    int currentStock, 
                    int unitPriceAmount,
                    string brandId,
                    string warehouseId,
                    ProviderId? providerId = null)
    {
        ProductName = new ProductName(Enum.Parse<EBrandName>(brandName, true), 
            Enum.Parse<ELiquorType>(liquorType, true), 
                        additionalName);
        LiquorType = Enum.Parse<ELiquorType>(liquorType, true); ;
        CurrentStock = new ProductStock(currentStock);
        BrandId = brandId;
        UnitPrice = new Money(unitPriceAmount, new Currency("PEN"));
        ImageUrl = new ImageUrl(imageUrl);
        WarehouseId = warehouseId;
        ProviderId = providerId;
    }

    /// <summary>
    /// Method to handle the creation of a product with a CreateProductCommand command.
    /// </summary>
    public Product()
    {
        // TODO: Add handler for CreateProductCommand.
    }
}