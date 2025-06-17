using StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Entities;

/// <summary>
/// Represents a brand of a product in the inventory management system.
/// </summary>
/// <param name="name">
/// The name of the brand.
/// </param>
public class Brand(EBrandName name)
{
    /// <summary>
    /// The unique identifier for the brand.
    /// </summary>
    public string Id { get; set; } = Guid.NewGuid().ToString();
    
    /// <summary>
    /// The name of the brand.
    /// </summary>
    public EBrandName Name { get; set; } = name;
}