using System.Runtime.InteropServices.JavaScript;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;
using StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Entities;

/// <summary>
/// Represents a product exit in the inventory management system.
/// </summary>
public partial class ProductExit
{
    /// <summary>
    /// The unique identifier for the product exit, generated as a new GUID.
    /// </summary>
    public string Id { get; } = Guid.NewGuid().ToString();
    
    /// <summary>
    /// The inventory of the product associated with the exit, represented as an Inventory entity.
    /// </summary>
    public Inventory Inventory { get; internal set; }
    
    /// <summary>
    /// The unique identifier of the product associated with the exit.
    /// </summary>
    public string ProductId { get; private set; }
    
    /// <summary>
    /// The unique identifier of the warehouse from which the product is exiting.
    /// </summary>
    public string WarehouseId { get; private set; }
    
    /// <summary>
    /// The reason for the product exit, represented as an enumeration of exit reasons.
    /// </summary>
    public EProductExitReasons ExitReason { get; private set; }
    
    /// <summary>
    /// The quantity of the product being exited from the inventory.
    /// </summary>
    public int ProductQuantity { get; private set; }
    
    /// <summary>
    /// The expiration date of the product associated with the exit, represented as a ProductExpirationDate value object.
    /// </summary>
    public ProductExpirationDate ProductExpirationDate { get; private set; }
    
    /// <summary>
    /// The date and time when the product exit occurred.
    /// </summary>
    public DateTime ExitDate { get; } = DateTime.Now;

    /// <summary>
    /// Default constructor for the ProductExit class, required by EF Core.
    /// </summary>
    /// <param name="productId">
    /// The unique identifier of the product associated with the exit.
    /// </param>
    /// <param name="warehouseId">
    /// The unique identifier of the warehouse from which the product is exiting.
    /// </param>
    /// <param name="exitReason">
    /// The reason for the product exit, represented as a string that will be parsed into an EProductExitReasons enumeration.
    /// </param>
    /// <param name="productQuantity">
    /// The quantity of the product being exited from the inventory.
    /// </param>
    public ProductExit(string productId, string warehouseId, string exitReason, int productQuantity, DateTime productExpirationDate)
    {
        ProductId = productId;
        WarehouseId = warehouseId;
        ExitReason = Enum.Parse<EProductExitReasons>(exitReason);
        ProductQuantity = productQuantity;
        ProductExpirationDate = new ProductExpirationDate(productExpirationDate);
    }

    /// <summary>
    /// Method to handle the creation of a product exit with a RegisterProductExitCommand command.
    /// </summary>
    public ProductExit(RegisterProductExitCommand command)
    {
        ProductId = command.ProductId;
        WarehouseId = command.WarehouseId;
        ExitReason = Enum.Parse<EProductExitReasons>(command.ExitReason);
        ProductQuantity = command.QuantityExited;
        ProductExpirationDate = new ProductExpirationDate(command.ExpirationDate);
    }

    /// <summary>
    /// Method to update the inventory after a product exit.
    /// </summary>
    /// <param name="quantityExited">
    /// The quantity of the product that has exited the inventory.
    /// </param>
    public void UpdateInventoryAfterExit(int quantityExited)
    {
        Inventory.RemoveStockFromProduct(quantityExited);
    }
}