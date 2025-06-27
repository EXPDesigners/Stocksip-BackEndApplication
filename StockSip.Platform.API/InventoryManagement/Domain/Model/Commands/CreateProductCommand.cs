using StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;

/// <summary>
/// This command represents the creation of a new product.
/// </summary>
/// <param name="Name">
/// This is an optional field that can be used to provide additional information about the product.
/// </param>
/// <param name="LiquorType">
/// This field specifies the type of liquor for the product, such as 'Whiskey', 'Vodka', etc.
/// </param>
/// <param name="BrandName">
/// This field specifies the brand name of the product.
/// </param>
/// <param name="UnitPriceAmount">
/// This field specifies the unit price of the product.
/// </param>
/// <param name="MinimumStock">
/// This field specifies the minimum stock level for the product.
/// </param>
/// <param name="ImageUrl">
/// This field specifies the URL of the product image.
/// </param>
/// <param name="ProviderId">
/// This field specifies the ID of the provider for the product. It can be null if the product does not have a provider.
/// </param>
public record CreateProductCommand(string? Name, 
                                    string LiquorType, 
                                    string BrandName, 
                                    double UnitPriceAmount,
                                    int MinimumStock,
                                    IFormFile? Image,
                                    string? ProviderId);