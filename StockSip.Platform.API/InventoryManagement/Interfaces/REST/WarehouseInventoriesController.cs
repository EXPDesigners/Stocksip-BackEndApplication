using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Queries;
using StockSip.Platform.API.InventoryManagement.Domain.Services;
using StockSip.Platform.API.InventoryManagement.Interfaces.REST.Resources;
using StockSip.Platform.API.InventoryManagement.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace StockSip.Platform.API.InventoryManagement.Interfaces.REST;

[ApiController]
[Route("api/v1/warehouses/{warehouseId}/inventories")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Warehouses")]
public class WarehouseInventoriesController (
    IInventoryCommandService inventoryCommandService,
    IInventoryQueryService inventoryQueryService
    ) : ControllerBase
{
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all products by warehouse ID",
        Description = "Retrieves all products associated with a specific warehouse ID.",
        OperationId = "GetProductsByWarehouseId")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of products found!", typeof(IEnumerable<ProductInventoryResource>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "No products found for the specified warehouse ID...")]
    public async Task<IActionResult> GetProductsByWarehouseId(string warehouseId)
    {
        var getAllProductsByWarehouseIdQuery = new GetAllProductsByWarehouseIdQuery(warehouseId);
        var products = await inventoryQueryService.Handle(getAllProductsByWarehouseIdQuery);
        var productResources = products
            .Select(ProductInventoryResourceFromEntityAssembler.ToResourceFromEntity)
            .ToList();
        
        return Ok(productResources);
    }
    
    [HttpGet("product/{productId}/expiration-date/{expirationDate:datetime}")]
    [SwaggerOperation(
        Summary = "Get an inventory by its product ID, warehouse ID and expiration date",
        Description = "Retrieves an inventory by its product ID, warehouse ID, and expiration date.",
        OperationId = "GetInventoryByProductIdAndWarehouseIdAndBestBeforeDate")]
    [SwaggerResponse(StatusCodes.Status200OK, "Inventory found!", typeof(InventoryResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Inventory not found for the specified ID, warehouse ID, and expiration date...")]
    public async Task<IActionResult> GetInventoryByProductIdAndWarehouseIdAndBestBeforeDate(
        string productId, 
        string warehouseId,
        DateOnly expirationDate)
    {
        var getInventoryByIdAndWarehouseIdAndExpirationDateQuery =
            new GetInventoryByProductIdAndWarehouseIdAndBestBeforeDateQuery(productId, warehouseId, expirationDate);
        var inventory = await inventoryQueryService.Handle(getInventoryByIdAndWarehouseIdAndExpirationDateQuery);
        if (inventory is null)
        {
            return NotFound($"Inventory for product with ID {productId} and with expiration date {expirationDate} not found in warehouse {warehouseId}.");
        }
        var inventoryResource = InventoryResourceFromEntityAssembler.ToResourceFromEntity(inventory);
        return Ok(inventoryResource);
    }
    
    [HttpPut("product/{productId}/moves")]
    [SwaggerOperation(
        Summary = "Move products to another warehouse",
        Description =
            "Moves products from one warehouse to another by their product ID, source warehouse ID, and destination warehouse ID. Then creates a new inventory or updates the existing one.",
        OperationId = "MoveProductsToAnotherWarehouse")]
    [SwaggerResponse(StatusCodes.Status201Created, "Stock moved to another warehouse successfully!", typeof(InventoryResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Stock not found in the specified source warehouse...")]
    public async Task<IActionResult> MoveProductToAnotherWarehouse(
        [FromBody] MoveProductsToAnotherWarehouseResource resource,
        string warehouseId,
        string productId)
    {
        var moveProductsToAnotherWarehouseCommand =
            MoveProductsToAnotherWarehouseCommandFromResourceAssembler.ToCommandFromResource(
                resource, productId, warehouseId);
        var movedStock = await inventoryCommandService.Handle(moveProductsToAnotherWarehouseCommand);
        if (movedStock is null)
        {
            return NotFound($"Inventory of product ID {productId} not found in warehouse {warehouseId} with expiration date {resource.MovedStockExpirationDate}. So products cannot be moved.");
        }
        var inventoryResource = InventoryResourceFromEntityAssembler.ToResourceFromEntity(movedStock);
        return CreatedAtAction(nameof(GetInventoryByProductIdAndWarehouseIdAndBestBeforeDate), 
            new { productId, resource.NewWarehouseId, resource.MovedStockExpirationDate }, 
            inventoryResource);
    }
    
    [HttpPut("product/{productId}/additions")]
    [SwaggerOperation(
        Summary = "Add stock to a product in a warehouse",
        Description = "Adds stock to a product in a warehouse by its product ID, warehouse ID, and expiration date.",
        OperationId = "AddStockToProduct")]
    [SwaggerResponse(StatusCodes.Status201Created, "Stock added successfully!", typeof(InventoryResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Product not found in the specified warehouse...")]
    public async Task<IActionResult> AddStockToProduct(
        [FromBody] AddStockToProductResource resource,
        string productId,
        string warehouseId)
    {
        Console.WriteLine($"📥 AddStockToProduct:");
        Console.WriteLine($"➡ productId: {productId}");
        Console.WriteLine($"➡ warehouseId: {warehouseId}");
        Console.WriteLine($"➡ stockExpirationDate: {resource.StockExpirationDate}");
        Console.WriteLine($"➡ addedQuantity: {resource.AddedQuantity}");
        
        var addStockCommand = AddStockToProductCommandFromResourceAssembler.ToCommandFromResource(resource, productId, warehouseId);
        var updatedInventory = await inventoryCommandService.Handle(addStockCommand);
        if (updatedInventory is null)
        {
            return NotFound($"Product with ID {productId} not found in warehouse {warehouseId} with expiration date {resource.StockExpirationDate}. So stock cannot be added.");
        }
        var inventoryResource = InventoryResourceFromEntityAssembler.ToResourceFromEntity(updatedInventory);
        return CreatedAtAction(nameof(GetInventoryByProductIdAndWarehouseIdAndBestBeforeDate), 
            new { productId = productId, warehouseId = warehouseId, expirationDate = resource.StockExpirationDate }, 
            inventoryResource);
    }
    
    [HttpPut("product/{productId}/substractions")]
    [SwaggerOperation(
        Summary = "Decrease stock from a product in a warehouse",
        Description = "Decreases stock from a product in a warehouse by its product ID, warehouse ID, and expiration date.",
        OperationId = "DecreaseStockFromProduct")]
    [SwaggerResponse(StatusCodes.Status201Created, "Stock decreased successfully!", typeof(InventoryResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Product not found in the specified warehouse...")]
    public async Task<IActionResult> DecreaseStockFromProduct(
        [FromBody] DecreaseStockFromProductResource resource,
        string productId,
        string warehouseId)
    {
        var decreaseStockCommand = DecreaseStockFromProductCommandFromResourceAssembler.ToCommandFromResource(resource, productId, warehouseId);
        var updatedInventory = await inventoryCommandService.Handle(decreaseStockCommand);
        if (updatedInventory is null)
        {
            return NotFound($"Product with ID {productId} not found in warehouse {warehouseId} with expiration date {resource.ExpirationDate}. So stock cannot be decreased.");
        }
        
        var inventoryResource = InventoryResourceFromEntityAssembler.ToResourceFromEntity(updatedInventory);
        
        return CreatedAtAction(nameof(GetInventoryByProductIdAndWarehouseIdAndBestBeforeDate), 
            new { productId = productId, warehouseId = warehouseId, expirationDate = resource.ExpirationDate }, 
            inventoryResource);
    }
    
    [HttpPost("product/{productId}")]
    [SwaggerOperation(
        Summary = "Add stock to a product in a warehouse",
        Description = "Adds stock to a product in a warehouse by its product ID and warehouse ID, including the expiration date.",
        OperationId = "AddProductToWarehouse")]
    [SwaggerResponse(StatusCodes.Status201Created, "Products added to warehouse successfully!", typeof(InventoryResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Products could not be added to warehouse...")]
    public async Task<IActionResult> AddProductToWarehouse(
        [FromBody] AddProductsToWarehouseResource resource,
        string productId,
        string warehouseId)
    {
        Console.WriteLine($"📥 AddStockToProduct:");
        Console.WriteLine($"➡ productId: {productId}");
        Console.WriteLine($"➡ warehouseId: {warehouseId}");
        Console.WriteLine($"➡ stockExpirationDate: {resource.ExpirationDate}");
        Console.WriteLine($"➡ addedQuantity: {resource.Quantity}");
        var addProductToWarehouseCommand = 
            AddProductsToWarehouseCommandFromResourceAssembler.ToCommandFromResource(resource, productId, warehouseId);
        var productWithAddedStock = await inventoryCommandService.Handle(addProductToWarehouseCommand);
        if (productWithAddedStock is null)
        {
            return BadRequest($"Failed to add products to warehouse {warehouseId} for product with ID {productId}. Please check the provided data.");
        }
        
        var inventoryResource = InventoryResourceFromEntityAssembler.ToResourceFromEntity(productWithAddedStock);
        return CreatedAtAction(nameof(GetInventoryByProductIdAndWarehouseIdAndBestBeforeDate), 
            new { productId, warehouseId, resource.ExpirationDate}, 
            inventoryResource);
    }
    
    [HttpDelete("product/{productId}")]
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
        var deletedProduct = await inventoryCommandService.Handle(deleteProductCommand);
        if (deletedProduct is null)
        {
            return NotFound($"Product with ID {productId} not found in warehouse {warehouseId} with expiration date {resource.ExpirationDate}.");
        }
        return Ok($"Product with ID {productId} successfully deleted from warehouse {warehouseId} with expiration date {resource.ExpirationDate}.");
    }
}