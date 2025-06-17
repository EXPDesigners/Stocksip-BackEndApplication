using System.Runtime.InteropServices.JavaScript;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Entities;

/// <summary>
/// Represents a product exit in the inventory management system.
/// </summary>
public partial class ProductExit
{
    /// <summary>
    /// The product associated with the exit, represented as a Product entity.
    /// </summary>
    public Product Product { get; internal set; }
    
    /// <summary>
    /// The unique identifier of the product associated with the exit.
    /// </summary>
    public string ProductId { get; private set; }
    
    /// <summary>
    /// The reason for the product exit, represented as an enumeration of exit reasons.
    /// </summary>
    public EProductExitReasons ExitReason { get; private set; }
    
    /// <summary>
    /// The quantity of the product being exited from the inventory.
    /// </summary>
    public int ProductQuantity { get; private set; }
    
    /// <summary>
    /// The date and time when the product exit occurred.
    /// </summary>
    public DateTime ExitDate { get; private set; } = DateTime.Now;

    public ProductExit(string productId, string exitReason, int productQuantity)
    {
        ProductId = productId;
        ExitReason = Enum.Parse<EProductExitReasons>(exitReason);
        ProductQuantity = productQuantity;
    }

    /// <summary>
    /// Method to handle the creation of a product exit with a CreateProductExitCommand command.
    /// </summary>
    public ProductExit()
    {
        //TODO: Implement the logic to handle the creation of a product exit.
    }
}