namespace StockSip.Platform.API.InventoryManagement.Interfaces.REST.Resources;

/// <summary>
/// This record defines the resource for creating a new product.
/// </summary>
public record CreateProductResource(string? Name, 
                                    string LiquorType, 
                                    string BrandName, 
                                    decimal UnitPriceAmount,
                                    int MinimumStock,
                                    IFormFile? Image);