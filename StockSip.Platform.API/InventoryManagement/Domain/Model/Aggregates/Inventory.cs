using StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Events;
using StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.InventoryManagement.Domain.Model.Aggregates;

/// <summary>
/// This class represents an Inventory aggregate in the domain model.
/// It encapsulates the details of a product's stock in a specific warehouse.
/// This entity is generated from the relationship between the Product and Warehouse aggregates.
/// </summary>
public class Inventory
{
    /// <summary>
    /// The unique identifier of the inventory.
    /// </summary>
    public string InventoryId { get; private set; } = Guid.NewGuid().ToString();

    /// <summary>
    /// The Product associated with the inventory, represented as a Product entity.
    /// </summary>
    public Product Product { get; internal set; }
    
    /// <summary>
    /// The unique identifier of the product in the inventory.
    /// </summary>
    public string ProductId { get; set; }
    
    /// <summary>
    /// The Warehouse where the inventory is located, represented as a Warehouse entity.
    /// </summary>
    public Warehouse Warehouse { get; internal set; }
    
    /// <summary>
    /// The unique identifier of the warehouse where the inventory is stored.
    /// </summary>
    public string WarehouseId { get; private set; }
    
    /// <summary>
    /// The current stock of the product, represented as a ProductStock value object.
    /// </summary>
    public ProductStock ProductStock { get; internal set; }

    /// <summary>
    /// The state of the product in the inventory, represented as an enumeration of type EProductState.
    /// </summary>
    public EProductState ProductState { get; internal set; } = EProductState.WithStock;
    
    /// <summary>
    /// The expiration date of the product, represented as a value object.
    /// </summary>
    public ProductBestBeforeDate ProductBestBeforeDate { get; internal set; }
    
    /// <summary>
    /// Default constructor for Entity Framework Core.
    /// </summary>
    private Inventory() { }
    
    /// <summary>
    /// Default constructor for the Inventory class.
    /// </summary>
    /// <param name="warehouseId">
    /// The unique identifier of the warehouse where the inventory is stored.
    /// </param>
    /// <param name="productId">
    /// The unique identifier of the product in the inventory.
    /// </param>
    /// <param name="expirationDate">
    /// The expiration date of the batch of products, represented as a DateTime value.
    /// </param>
    /// <param name="stock">
    /// The initial stock of the product in the inventory, represented as an integer.
    /// </param>
    public Inventory(string warehouseId, string productId, DateOnly expirationDate, int stock)
    {
        WarehouseId = warehouseId;
        ProductId = productId;
        ProductBestBeforeDate = new ProductBestBeforeDate(expirationDate);
        ProductStock = new ProductStock(stock);
    }

    /// <summary>
    /// Command handler constructor for the AddProductsToWarehouseCommand.
    /// </summary>
    /// <param name="command">
    /// The command that contains the necessary information to create an inventory entry.
    /// </param>
    public Inventory(AddProductsToWarehouseCommand command)
    {
        WarehouseId = command.WarehouseId;
        ProductId = command.ProductId;
        ProductBestBeforeDate = new ProductBestBeforeDate(command.BestBeforeDate);
        ProductStock = new ProductStock(command.Quantity);
    }

    /// <summary>
    /// Marks the product as out of stock.
    /// </summary>
    /// <exception cref="ArgumentException">
    /// Thrown when the product is already out of stock.
    /// </exception>
    private void SetProductStateToOutOfStock()
    {
        // Check if the product is already out of stock
        if (ProductState == EProductState.OutOfStock)
        {
            throw new ArgumentException("Product is already out of stock");
        }
        
        // Set the product state to out of stock
        ProductState = EProductState.OutOfStock;
    }

    /// <summary>
    /// Marks the product as with stock.
    /// </summary>
    /// <exception cref="ArgumentException">
    /// Thrown when the product is already marked as with stock.
    /// </exception>
    private void SetProductStateToWithStock()
    {
        // Check if the product is already marked as with stock
        if (ProductState == EProductState.WithStock)
        {
            throw new ArgumentException("Product is already marked as with stock");
        }
        
        // Set the product state to with stock
        ProductState = EProductState.WithStock;
    }
    
    /// <summary>
    /// Adds stock to the product in the inventory.
    /// </summary>
    /// <param name="addedStock">
    /// The amount of stock to be added to the product.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown when the added stock is less than or equal to zero.
    /// </exception>
    public void AddStockToProduct(int addedStock)
    {
        if (addedStock <= 0)
        {
            throw new ArgumentException("Stock cannot be negative");
        }

        if (ProductStock.GetCurrentStock() == 0)
        {
            SetProductStateToWithStock();
        }

        var currentStock = ProductStock.GetCurrentStock();
        ProductStock = ProductStock.IncreaseStock(addedStock);
    }

    /// <summary>
    /// Removes stock from the product in the inventory.
    /// </summary>
    /// <param name="removedStock">
    /// The amount of stock to be removed from the product.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown when the removed stock is less than or equal to zero, or when there is insufficient stock to remove.
    /// </exception>
    public void RemoveStockFromProduct(int removedStock)
    {
        // Validate the removed stock amount
        if (removedStock <= 0)
        {
            throw new ArgumentException("Stock cannot be negative");
        }
        
        // Check if there are enough stocks to remove
        if (ProductStock.GetCurrentStock() < removedStock)
        {
            throw new ArgumentException("Insufficient stock to remove");
        }

        // Checks if the product is below minimum stock after removal. If so, it should trigger a domain event to generate an alert.
        if (Product.MinimumStock.GetMinimumStock() >= ProductStock.GetCurrentStock() - removedStock)
        {
            // Create a domain event to notify about the product problem by creating an alert in the alerts and notifications context.
            var productProblemEvent = new ProductProblemDetectedEvent(
                "Product Stock Alert",
                $"The stock of product {ProductId} in warehouse {WarehouseId} is below the minimum threshold.",
                "Warning",
                "ProductLowStock",
                Warehouse.AccountId.Id,
                ProductId,
                WarehouseId
            );
        }
        
        // Decrease the stock of the product
        ProductStock = ProductStock.DecreaseStock(removedStock);

        // If the stock reaches zero after removal, change the product state to out of stock
        if (ProductStock.GetCurrentStock() == 0)
        {
            SetProductStateToOutOfStock();
        }
    }
    
    /// <summary>
    /// Moves the product to another warehouse.
    /// </summary>
    /// <param name="newWarehouseId">
    /// The unique identifier of the new warehouse where the product will be moved.
    /// </param>
    /// <exception cref="ArgumentException">
    /// Thrown when the new warehouse ID is null or the same as the current warehouse ID.
    /// </exception>
    public void MoveProductToAnotherWarehouse(string newWarehouseId)
    {
        // Validates the new warehouse ID
        if (newWarehouseId == null)
        {
            throw new ArgumentException("New warehouse ID cannot be null");
        } 
        
        // Check if the new warehouse ID is the same as the current warehouse ID
        if (newWarehouseId == WarehouseId)
        {
            throw new ArgumentException("New warehouse ID cannot be the same as the current warehouse ID");
        }
        
        // Update the warehouse ID to the new warehouse
        WarehouseId = newWarehouseId;
    }
}