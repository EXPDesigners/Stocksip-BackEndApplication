using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Queries;
using StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;
using StockSip.Platform.API.InventoryManagement.Domain.Services;
using StockSip.Platform.API.InventoryManagement.Interfaces.REST.Resources;
using StockSip.Platform.API.InventoryManagement.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace StockSip.Platform.API.InventoryManagement.Interfaces.REST;

[ApiController]
[Route("api/v1/warehouses/{warehouseId}/products")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Warehouses")]
public class WarehouseProductsController(
    IProductCommandService productCommandService,
    IProductQueryService productQueryService) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all products by warehouse ID",
        Description = "Retrieves all products associated with a specific warehouse ID.",
        OperationId = "GetProductsByWarehouseId")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of products found!", typeof(IEnumerable<ProductResource>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Products with warehouse ID not found...", typeof(IEnumerable<ProductResource>))]
    public async Task<IActionResult> GetProductsByWarehouseId(string warehouseId)
    {
        var getAllProductsByWarehouseIdQuery = new GetAllProductsByWarehouseIdQuery(warehouseId);
        var products = await productQueryService.Handle(getAllProductsByWarehouseIdQuery);
        var productResources = products
            .Select(ProductResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(productResources);
    }
    
    /// <summary>
    /// This endpoint retrieves all products associated with a specific Provider and Warehouse ID.
    /// </summary>
    /// <param name="providerId">
    /// The unique identifier of the provider whose products are to be retrieved.
    /// </param>
    /// <param name="warehouseId">
    /// The unique identifier of the warehouse from which products are to be retrieved.
    /// </param>
    /// <returns>
    /// An IActionResult containing a list of product resources if found, or a NotFound result if no products are found for the specified provider and warehouse ID.
    /// </returns>
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all products by provider and warehouse ID",
        Description = "Retrieves all products with a specific Provider and Warehouse ID.",
        OperationId = "GetAllProductsByProviderIdAndWarehouseId")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of products found!", typeof(IEnumerable<ProductResource>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "No products found for the specified provider and warehouse ID...")]
    public async Task<IActionResult> GetAllProductsByProviderIdAndWarehouseId(
        string providerId, 
        string warehouseId)
    {
        var providerIdentifier = new ProviderId(providerId);
        var products = await productQueryService.Handle(new GetAllProductsByProviderIdAndWarehouseIdQuery(providerIdentifier, warehouseId));
        var productsEnumerable = products.ToList();
        if (productsEnumerable.Count == 0)
        {
            return NotFound($"No products found for provider with ID {providerId}.");
        }
        var productsResource = productsEnumerable.Select(ProductResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(productsResource);
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all products by warehouse ID and full name",
        Description = "Retrieves all products associated with a specific warehouse ID and full name (brand, liquor type, and additional name).",
        OperationId = "GetAllProductsByWarehouseIdAndFullName")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of products found!", typeof(IEnumerable<ProductResource>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "No products found for the specified warehouse ID and full name...")]
    public async Task<IActionResult> GetAllProductsByWarehouseIdAndFullName(
        string warehouseId, 
        string brandName,
        string liquorType, 
        string? additionalName)
    {
        var getAllProductsByFullNameAndWarehouseIdQuery =
            new GetProductsByFullNameAndWarehouseIdQuery(warehouseId, brandName, liquorType, additionalName);
        var products = await productQueryService.Handle(getAllProductsByFullNameAndWarehouseIdQuery);
        var productsEnumerable = products.ToList();
        if (productsEnumerable.Count == 0)
        {
            return NotFound($"No products found in warehouse {warehouseId} with the specified full name {brandName} {liquorType} {additionalName}.");
        }
        var productsResource = productsEnumerable.Select(ProductResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(productsResource);
    }

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get a product by its ID, warehouse ID and expiration date",
        Description = "Retrieves a product by its ID, warehouse ID, and expiration date.",
        OperationId = "GetProductByIdAndWarehouseIdAndExpirationDate")]
    [SwaggerResponse(StatusCodes.Status200OK, "Product found!", typeof(ProductResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Product not found for the specified ID, warehouse ID, and expiration date...")]
    public async Task<IActionResult> GetProductByIdAndWarehouseIdAndExpirationDate(
        string productId, 
        string warehouseId,
        DateTime expirationDate)
    {
        var getProductByIdAndWarehouseIdAndExpirationDateQuery =
            new GetProductByIdAndWarehouseIdAndExpirationDateQuery(productId, warehouseId, expirationDate);
        var product = await productQueryService.Handle(getProductByIdAndWarehouseIdAndExpirationDateQuery);
        if (product is null)
        {
            return NotFound($"Product with ID {productId} not found in warehouse {warehouseId} with expiration date {expirationDate}.");
        }
        var productResource = ProductResourceFromEntityAssembler.ToResourceFromEntity(product);
        return Ok(productResource);
    }

    [HttpDelete]
    [SwaggerOperation(
        Summary = "Deletes a product from a warehouse",
        Description = "Deletes a product from a warehouse by its ID, warehouse ID, and expiration date.",
        OperationId = "DeleteProductFromWarehouse")]
    [SwaggerResponse(StatusCodes.Status200OK, "Product successfully deleted from warehouse!", typeof(string))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Product cannot be deleted because it was not found in the specified warehouse...")]
    public async Task<IActionResult> DeleteProduct(
        [FromBody] DeleteProductFromWarehouseResource resource,
        string productId, 
        string warehouseId)
    {
        var deleteProductCommand = DeleteProductFromWarehouseCommandFromResourceAssembler.ToCommandFromResource(resource, productId, warehouseId);
        var deletedProduct = await productCommandService.Handle(deleteProductCommand);
        if (deletedProduct is null)
        {
            return NotFound($"Product with ID {productId} not found in warehouse {warehouseId} with expiration date {resource.ExpirationDate}.");
        }
        return Ok($"Product with ID {productId} successfully deleted from warehouse {warehouseId} with expiration date {resource.ExpirationDate}.");
    }

    [HttpPut]
    [SwaggerOperation(
        Summary = "Add stock to a product in a warehouse",
        Description = "Adds stock to a product in a warehouse by its product ID and warehouse ID, including the expiration date.",
        OperationId = "AddProductsToWarehouse")]
    [SwaggerResponse(StatusCodes.Status201Created, "Products added to warehouse successfully!", typeof(ProductResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Products could not be added to warehouse...")]
    public async Task<IActionResult> AddProductsToWarehouse(
        [FromBody] AddProductsToWarehouseResource resource,
        string productId,
        string warehouseId)
    {
        var addProductToWarehouseCommand = 
            AddProductsToWarehouseCommandFromResourceAssembler.ToCommandFromResource(resource, productId, warehouseId);
        var productWithAddedStock = await productCommandService.Handle(addProductToWarehouseCommand);
        if (productWithAddedStock is null)
        {
            return BadRequest($"Failed to add products to warehouse {warehouseId} for product with ID {productId}. Please check the provided data.");
        }
        var productResource = ProductResourceFromEntityAssembler.ToResourceFromEntity(productWithAddedStock);
        return CreatedAtAction(nameof(GetProductByIdAndWarehouseIdAndExpirationDate), 
            new { productId, warehouseId, resource.ExpirationDate}, 
            productResource);
    }
    
    [HttpPut]
    [SwaggerOperation(
        Summary = "Decrease stock from a product in a warehouse",
        Description = "Decreases stock from a product in a warehouse by its product ID, warehouse ID, and expiration date.",
        OperationId = "DecreaseStockFromProduct")]
    [SwaggerResponse(StatusCodes.Status201Created, "Stock decreased successfully!", typeof(ProductResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Product not found in the specified warehouse...")]
    public async Task<IActionResult> DecreaseStockFromProduct(
        [FromBody] DecreaseStockFromProductResource resource,
        string productId,
        string warehouseId)
    {
        var decreaseStockCommand = DecreaseStockFromProductCommandFromResourceAssembler.ToCommandFromResource(resource, productId, warehouseId);
        var updatedInventory = await productCommandService.Handle(decreaseStockCommand);
        if (updatedInventory is null)
        {
            return NotFound($"Product with ID {productId} not found in warehouse {warehouseId} with expiration date {resource.ExpirationDate}. So stock cannot be decreased.");
        }
        
        var productResource = ProductResourceFromEntityAssembler.ToResourceFromEntity(updatedInventory.Product);
        
        return CreatedAtAction(nameof(GetProductByIdAndWarehouseIdAndExpirationDate), 
            new { productId, warehouseId, resource.ExpirationDate}, 
            productResource);
    }
    
    [HttpPut]
    [SwaggerOperation(
        Summary = "Add stock to a product in a warehouse",
        Description = "Adds stock to a product in a warehouse by its product ID, warehouse ID, and expiration date.",
        OperationId = "AddStockToProduct")]
    [SwaggerResponse(StatusCodes.Status201Created, "Stock decreased successfully!", typeof(ProductResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Product not found in the specified warehouse...")]
    public async Task<IActionResult> AddStockToProduct(
        [FromBody] AddStockToProductResource resource,
        string productId,
        string warehouseId)
    {
        var addStockCommand = AddStockToProductCommandFromResourceAssembler.ToCommandFromResource(resource, productId, warehouseId);
        var updatedInventory = await productCommandService.Handle(addStockCommand);
        if (updatedInventory is null)
        {
            return NotFound($"Product with ID {productId} not found in warehouse {warehouseId} with expiration date {resource.StockExpirationDate}. So stock cannot be added.");
        }
        var productResource = ProductResourceFromEntityAssembler.ToResourceFromEntity(updatedInventory.Product);
        return CreatedAtAction(nameof(GetProductByIdAndWarehouseIdAndExpirationDate), 
            new { productId, warehouseId, resource.StockExpirationDate}, 
            productResource);
    }

    [HttpPut]
    [SwaggerOperation(
        Summary = "Move products to another warehouse",
        Description =
            "Moves products from one warehouse to another by their product ID, source warehouse ID, and destination warehouse ID.",
        OperationId = "MoveProductsToAnotherWarehouse")]
    [SwaggerResponse(StatusCodes.Status201Created, "Products moved to another warehouse successfully!", typeof(ProductResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Product not found in the specified source warehouse...")]
    public async Task<IActionResult> MoveProductsToAnotherWarehouse(
        [FromBody] MoveProductsToAnotherWarehouseResource resource,
        string warehouseId,
        string productId)
    {
        var moveProductsToAnotherWarehouseCommand =
            MoveProductsToAnotherWarehouseCommandFromResourceAssembler.ToCommandFromResource(
                resource, productId, warehouseId);
        var movedProduct = await productCommandService.Handle(moveProductsToAnotherWarehouseCommand);
        if (movedProduct is null)
        {
            return NotFound($"Product with ID {productId} not found in warehouse {warehouseId} with expiration date {resource.MovedStockExpirationDate}. So products cannot be moved.");
        }
        var productResource = ProductResourceFromEntityAssembler.ToResourceFromEntity(movedProduct);
        return CreatedAtAction(nameof(GetProductByIdAndWarehouseIdAndExpirationDate), 
            new { productId, resource.NewWarehouseId, resource.MovedStockExpirationDate }, 
            productResource);
    }
}