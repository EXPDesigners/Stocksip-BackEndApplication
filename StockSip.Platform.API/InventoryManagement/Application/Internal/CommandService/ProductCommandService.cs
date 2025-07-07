using StockSip.Platform.API.InventoryManagement.Application.Internal.OutboundServices.Cloudinary;
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
    ICloudinaryService cloudinaryService,
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
        if (await productRepository.ExistsByFullNameIgnoreCaseAsync(command.BrandName, command.LiquorType, command.Name, command.AccountId))
        {
            throw new ArgumentException($"Product with full name {command.BrandName} {command.LiquorType} {command.Name} already exists.");
        }

        string imageUrl = command.Image != null
            ? cloudinaryService.UploadImage(command.Image)
            : "https://res.cloudinary.com/deuy1pr9e/image/upload/v1750999534/default-product_lcmtsm.jpg";
        
        var product = new Product(command, imageUrl);
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
        
        var currentImageUrl = await productRepository.FindImageUrlByProductIdAsync(command.ProductId);
        string imageUrl = currentImageUrl;

        if (command.UpdatedImage != null)
        {
            cloudinaryService.DeleteImage(currentImageUrl);
            imageUrl = cloudinaryService.UploadImage(command.UpdatedImage);
        }
        
        productToUpdate.UpdateInformation(
                command.Name,
                command.Brand,
                command.LiquorType,
                command.UpdatedUnitPriceAmount,
                command.UpdatedMinimumStock,
                imageUrl
            );
        
        productRepository.Update(productToUpdate);
        await unitOfWork.CompleteAsync();
        return productToUpdate;
    }

    /// <summary>
    /// This method handles the update of the minimum stock level for an existing product.
    /// </summary>
    /// <param name="command">The command containing the product ID and the new minimum stock level.</param>
    /// <returns>A <see cref="Product"/> object representing the updated product.</returns>
    /// <exception cref="ArgumentException">If the product with the specified ID does not exist.</exception>
    public async Task<Product?> Handle(UpdateProductMinimumStockCommand command)
    {
        var productToUpdate = await productRepository.FindByIdAsync(command.ProductId)
                              ?? throw new ArgumentException($"Product with ID {command.ProductId} does not exist.");
        
        productToUpdate.SetMinimumStock(command.NewMinimumStock);
        productRepository.Update(productToUpdate);
        await unitOfWork.CompleteAsync();
        return productToUpdate;
    }

    /// <summary>
    /// This method handles the deletion of a product.
    /// </summary>
    /// <param name="command">The command containing the product ID to be deleted.</param>
    /// <exception cref="ArgumentException">A product with the specified ID does not exist.</exception>
    public async Task Handle(DeleteProductCommand command)
    {
        var productToDelete = await productRepository.FindByIdAsync(command.ProductId)
                                ?? throw new ArgumentException($"Product with ID {command.ProductId} does not exist.");
        
        var imageUrl = await productRepository.FindImageUrlByProductIdAsync(command.ProductId);
        cloudinaryService.DeleteImage(imageUrl);
        
        productRepository.Remove(productToDelete);
        await unitOfWork.CompleteAsync();
    }
}