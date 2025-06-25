using StockSip.Platform.API.InventoryManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;
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
    IInventoryRepository inventoryRepository,
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

    public async Task<Product?> Handle(UpdateProductMinimumStockCommand command)
    {
        var productToUpdate = await productRepository.FindByIdAsync(command.ProductId)
                              ?? throw new ArgumentException($"Product with ID {command.ProductId} does not exist.");
        
        productToUpdate.SetMinimumStock(command.NewMinimumStock);
        productRepository.Update(productToUpdate);
        await unitOfWork.CompleteAsync();
        return productToUpdate;
    }
}