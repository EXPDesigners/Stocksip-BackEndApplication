namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;

/// <summary>
/// This command is used to update the details of a product in the warehouse.
/// </summary>
/// <param name="ProductId">
/// The unique identifier of the product to be updated.
/// </param>
/// <param name="UpdatedUnitPriceAmount">
/// The new unit price amount for the product.
/// </param>
/// <param name="UpdatedMinimumStock">
/// The new minimum stock level for the product.
/// </param>
/// <param name="UpdatedImage">
/// The new image URL for the product.
/// </param>
public record UpdateProductCommand(string ProductId, 
                                   string Name, 
                                   string Brand, 
                                   string LiquorType, 
                                   decimal UpdatedUnitPriceAmount, 
                                   int UpdatedMinimumStock, 
                                   IFormFile? UpdatedImage);