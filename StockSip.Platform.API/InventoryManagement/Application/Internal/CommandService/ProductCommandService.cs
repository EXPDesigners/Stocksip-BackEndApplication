using Microsoft.EntityFrameworkCore.Storage;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Entities;
using StockSip.Platform.API.InventoryManagement.Domain.Repositories;
using StockSip.Platform.API.InventoryManagement.Domain.Services;
using StockSip.Platform.API.Shared.Domain.Repositories;

namespace StockSip.Platform.API.InventoryManagement.Application.Internal.CommandService;

/// <summary>
/// This class implements the command service for handling product-related commands.
/// </summary>
/// <param name="productRepository"> The repository for managing product and inventory data </param>
/// <param name="warehouseRepository"> The repository for managing warehouse data. </param>
/// <param name="unitOfWork">The unit of work for managing transactions in the database.</param>
public class ProductCommandService (
    IProductRepository productRepository,
    IWarehouseRepository warehouseRepository,
    IUnitOfWork unitOfWork
    ) : IProductCommandService
{
    /// <summary>
    /// This method handles the creation of a new product.
    /// </summary>
    /// <param name="command"> The command containing the details for creating a product.</param>
    /// <returns> The created product. </returns>
    /// <exception cref="ArgumentException"> Thrown when a product with the same name already exists.</exception>
    public async Task<Product?> Handle(CreateProductCommand command)
    {
        if (await productRepository.ExistsByFullNameIgnoreCase(command.BrandName, command.LiquorType, command.AdditionalName))
        {
            throw new ArgumentException($"Product with full name {command.BrandName} {command.LiquorType} {command.AdditionalName} already exists.");
        }
        
        var product = new Product(command);
        await productRepository.AddAsync(product);
        await unitOfWork.CompleteAsync();
        return product;
    }

    /// <summary>
    /// This method handles the update of an existing product.
    /// </summary>
    /// <param name="command"> The command containing the details for updating a product. </param>
    /// <returns> The updated product. </returns>
    /// <exception cref="ArgumentException"> Thrown when the product to be updated does not exist. </exception>
    public async Task<Product?> Handle(UpdateProductCommand command)
    {
        var productToUpdate = await productRepository.FindByIdAsync(command.ProductId)
                                ?? throw new ArgumentException($"Product with ID {command.ProductId} does not exist.");
        
        productToUpdate.UpdateInformation(
                command.UpdatedUnitPriceAmount,
                command.UpdatedMinimumStock,
                command.UpdatedImageUrl
            );
        
        productRepository.Update(productToUpdate);
        await unitOfWork.CompleteAsync();
        return productToUpdate;
    }

    /// <summary>
    /// Method to delete a product from a warehouse if the current stock is zero.
    /// </summary>
    /// <param name="command">
    /// The command containing the details for deleting a product from a warehouse.
    /// </param> The inventory entry after deletion, which may be a new or updated inventory.
    /// <returns>  </returns>
    /// <exception cref="ArgumentException"> Thrown when the product to be deleted or the warehouse
    /// where we try to delete the product or the inventory of the product in the warehouse does not exist. </exception>
    public async Task<Inventory?> Handle(DeleteProductFromWarehouseCommand command)
    {
        // Validate if the product to be deleted exists.
        var product = await productRepository.FindByIdAsync(command.ProductId)
                      ?? throw new ArgumentException($"Product with ID {command.ProductId} does not exist.");
        
        // Validate if the warehouse where the product will be deleted exists.
        var warehouse = await warehouseRepository.FindByIdAsync(command.WarehouseId)
                        ?? throw new ArgumentException($"Warehouse with ID {command.WarehouseId} does not exist.");
        
        // Retrieves the inventory of the product in the warehouse if it exists.
        var inventory = await productRepository.FindInventoryByProductIdAndWarehouseIdAndExpirationDateAsync(command.ProductId, command.WarehouseId, command.ExpirationDate)
                        ?? throw new ArgumentException($"Inventory with Product ID {command.ProductId}, Warehouse ID {command.WarehouseId} and Expiration Date {command.ExpirationDate} does not exist.");

        // If the retrieved inventory exists, it removes the relation between the product and the inventory.
        product.RemoveInventoryRelation(inventory);
        
        // If the current stock of the product in the warehouse is zero, it sets the product ID to an empty string to indicate that the product has been deleted from the warehouse.
        if (inventory.ProductStock.GetCurrentStock() == 0)
        {
            inventory.ProductId = "";
        }
        
        // If the current stock of the product in the warehouse is not zero, it throws an exception to prevent deletion.
        else
        {
            throw new ArgumentException("Cannot delete product from warehouse because the stock is not zero.");
        }
        
        // Completes the current inventory update by saving the changes to the database.
        await unitOfWork.CompleteAsync();
        
        // Returns the inventory entry after deletion, which may be a new or updated inventory.
        return inventory;
    }

    /// <summary>
    /// Method to add products to a new warehouse, creating a new inventory entry. This executes when creating a product in a warehouse.
    /// Also, this method won't be used with the same product in the same warehouse or in another.
    /// </summary>
    /// <param name="command"> The command containing the details for creating an inventory. </param>
    /// <returns> The new inventory which is a result of the relation between Product and Warehouse. </returns>
    /// <exception cref="ArgumentException"> Thrown when the product to be added or the warehouse
    /// where we try to add the product stock does not exist. </exception>
    public async Task<Inventory?> Handle(AddProductsToWarehouseCommand command)
    {
        // Validate if the product to be added exists.
        var product = await productRepository.FindByIdAsync(command.ProductId)
                        ?? throw new ArgumentException($"Product with ID {command.ProductId} does not exist.");
        
        // Validate if the warehouse where the product will be added exists.
        var warehouse = await warehouseRepository.FindByIdAsync(command.WarehouseId)
                        ?? throw new ArgumentException($"Warehouse with ID {command.WarehouseId} does not exist.");
        
        // Creates a new inventory entry for the product in the warehouse with the specified quantity.
        var inventory = new Inventory(command)
        {
            Product = product,
            Warehouse = warehouse
        };
        
        // Adds the new inventory entry to the product's inventory relations.
        product.AddInventoryRelation(inventory);
        
        // Completes the inventory creation by saving the changes to the database.
        await unitOfWork.CompleteAsync();
        
        // Returns the new inventory entry, which may be a new or updated inventory.
        return inventory;
    }
    
    /// <summary>
    /// Method to decrease the stock of a product in a warehouse, updating the existing inventory entry.
    /// </summary>
    /// <param name="command"> The command containing the details for decreasing the stock of the product in the warehouse. </param>
    /// <returns> The updated inventory of the product in the warehouse. </returns>
    /// <exception cref="ArgumentException"> Thrown when the product stock to be updated or the warehouse
    /// where we try to decrease the product stock or the inventory of the product in the warehouse does not exist. </exception>
    public async Task<Inventory?> Handle(DecreaseStockFromProductCommand command)
    {
        // Validate if the stock of the product to decrease exists.
        var product = await productRepository.FindByIdAsync(command.ProductId)
                      ?? throw new ArgumentException($"Product with ID {command.ProductId} does not exist.");
        
        // Validate if the warehouse where the stock will be decreased exists.
        var warehouse = await warehouseRepository.FindByIdAsync(command.WarehouseId)
                        ?? throw new ArgumentException($"Warehouse with ID {command.WarehouseId} does not exist.");
        
        // Retrieves the inventory of the product in the warehouse if it exists.
        var updatedInventory = await productRepository.FindInventoryByProductIdAndWarehouseIdAndExpirationDateAsync(command.ProductId, command.WarehouseId, command.ExpirationDate)
                               ?? throw new ArgumentException($"Inventory with Product ID {command.ProductId}, Warehouse ID {command.WarehouseId} and Expiration Date {command.ExpirationDate} does not exist.");

        // If the retrieved inventory exists, it decreases the stock to the current inventory.
        updatedInventory.RemoveStockFromProduct(command.RemovedQuantity);
        
        // Completes the current inventory update by saving the changes to the database.
        await unitOfWork.CompleteAsync();
        
        // Returns the updated inventory entry.
        return updatedInventory;
    }

    /// <summary>
    /// Method to add stock of a product in a warehouse, updating the existing inventory entry.
    /// </summary>
    /// <param name="command"> The command containing the details for adding the stock of the product in the warehouse. </param>
    /// <returns> The updated inventory of the product in the warehouse. </returns>
    /// <exception cref="ArgumentException"> Thrown when the product stock to be updated or the warehouse
    /// where we try to add the product stock or the inventory of the product in the warehouse does not exist. </exception>
    public async Task<Inventory?> Handle(AddStockToProductCommand command)
    {
        // Validate if the stock of the product to be added exists.
        var product = await productRepository.FindByIdAsync(command.ProductId)
            ?? throw new ArgumentException($"Product with ID {command.ProductId} does not exist.");
        
        // Validate if the warehouse where the stock will be added exists.
        var warehouse = await warehouseRepository.FindByIdAsync(command.WarehouseId)
            ?? throw new ArgumentException($"Warehouse with ID {command.WarehouseId} does not exist.");
        
        // Retrieves the inventory of the product in the warehouse if it exists, if not, it will create a new inventory entry with the new Expiration Date.
        var updatedInventory = await productRepository.FindInventoryByProductIdAndWarehouseIdAndExpirationDateAsync(command.ProductId, command.WarehouseId, command.StockExpirationDate);

        // If the retrieved inventory exists, it adds the stock to the current inventory.
        updatedInventory?.AddStockToProduct(command.AddedQuantity);
        
        // If the retrieved inventory does not exist, it creates a new inventory entry with the specified quantity and a different expiration date, but with the same product.
        updatedInventory ??= new Inventory(command.WarehouseId, command.ProductId, command.StockExpirationDate, command.AddedQuantity)
        {
            Product = product,
            Warehouse = warehouse
        };
        
        // Completes the current inventory update by saving the changes to the database.
        await unitOfWork.CompleteAsync();
        
        // Returns the updated inventory entry.
        return updatedInventory;
    }

    /// <summary>
    /// Method to move products from one warehouse to another, updating the stock of the current inventory and creating a new one if it does not exist.
    /// If it exists, this updates the stock of the inventory that will receive the products.
    /// </summary>
    /// <param name="command"> The command containing the details for moving the stock of a product in the warehouse to another one. </param>
    /// <returns> The updated or new inventory of the product in the new warehouse. </returns>
    /// <exception cref="ArgumentException"> Thrown when the product stock to be moved or the warehouse
    /// where we try to send the product stock does not exist. </exception>
    public async Task<Inventory?> Handle(MoveProductsToAnotherWarehouseCommand command)
    {
        // Validate if the product to be moved exists.
        var movedProduct = await productRepository.FindByIdAsync(command.ProductId)
                            ?? throw new ArgumentException($"Product with ID {command.ProductId} does not exist.");
        
        // Validate if the new warehouse where the product will be moved exists.
        var newWarehouse = await warehouseRepository.FindByIdAsync(command.NewWarehouseId)
                            ?? throw new ArgumentException($"Warehouse with ID {command.NewWarehouseId} does not exist.");

        // Retrieves the current inventory of the product in the old warehouse.
        var currentInventory = await productRepository.FindInventoryByProductIdAndWarehouseIdAndExpirationDateAsync(
                command.ProductId,
                command.OldWarehouseId,
                command.MovedStockExpirationDate) ?? throw new ArgumentException($"Inventory with Product ID {command.ProductId} and Warehouse ID {command.OldWarehouseId} does not exist.");

        // Removes the moved stock from the current inventory. And If the current inventory has no stock left, the product state will be set to OUT_OF_STOCK.
        currentInventory.RemoveStockFromProduct(command.MovedQuantity);
        
        // Retrieves the inventory of the product in the new warehouse if it exists.
        var newInventory = await productRepository.FindInventoryByProductIdAndWarehouseIdAndExpirationDateAsync(
            command.ProductId,
            command.NewWarehouseId,
            command.MovedStockExpirationDate);
        
        // If the retrieved inventory already existed, it adds the moved stock to the retrieved inventory.
        newInventory?.ProductStock.IncreaseStock(command.MovedQuantity);
        
        // If the retrieved inventory does not exist, creates a new inventory entry with the moved quantity.
        newInventory ??= new Inventory(command.NewWarehouseId, command.ProductId, command.MovedStockExpirationDate, command.MovedQuantity)
        {
            Product = movedProduct,
            Warehouse = newWarehouse
        };
        
        // Adds the new inventory entry to the product's inventory relations if it does not already exist.
        movedProduct.AddInventoryRelation(newInventory);
        
        // Completes the current inventory update by saving the changes to the database.
        await unitOfWork.CompleteAsync();
        
        // Returns the new inventory entry, which may be a new or updated inventory.
        return newInventory;
    }
}