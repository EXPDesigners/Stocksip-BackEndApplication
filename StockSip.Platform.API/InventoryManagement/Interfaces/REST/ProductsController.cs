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

/// <summary>
/// This controller provides endpoints for managing products.
/// </summary>
/// <param name="productCommandService">
/// The command service for handling product operations.
/// </param>
/// <param name="productQueryService">
/// The query service for retrieving product information.
/// </param>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Product Endpoints")]
public class ProductsController(
    IProductCommandService productCommandService,
    IProductQueryService productQueryService
    ) : ControllerBase
{
    /// <summary>
    /// This endpoint retrieves a product by its unique identifier.
    /// </summary>
    /// <param name="productId">
    /// The unique identifier of the product to be retrieved.
    /// </param>
    /// <returns>
    /// An IActionResult containing the product resource if found, or a NotFound result if not found.
    /// </returns>
    [HttpGet("{productId}")]
    [SwaggerOperation(
        Summary = "Get Product by ID",
        Description = "Retrieves a product by its unique identifier.",
        OperationId = "GetProductById")]
    [SwaggerResponse(StatusCodes.Status200OK, "Product found!", typeof(ProductInventoryResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Product not found...")]
    public async Task<IActionResult> GetProductById([FromRoute] string productId)
    {
        var product = await productQueryService.Handle(new GetProductByIdQuery(productId));
        if (product is null)
        {
            return NotFound($"Product with ID {productId} not found...");
        }
        var resource = ProductResourceFromEntityAssembler.ToResourceFromEntity(product);
        return Ok(resource);
    }

    /// <summary>
    /// This endpoint updates an existing product by its unique identifier.
    /// </summary>
    /// <param name="resource">
    /// The resource containing the updated product details.
    /// </param>
    /// <param name="productId">
    /// The unique identifier of the product to be updated.
    /// </param>
    /// <returns>
    /// An IActionResult indicating the result of the update operation, including the updated product resource if successful.
    /// </returns>
    [HttpPut("{productId}")]
    [SwaggerOperation(
        Summary = "Update an Existing Product",
        Description = "Updates an existing product by its unique identifier and returns the updated product resource.",
        OperationId = "UpdateProductInformation")]
    [SwaggerResponse(StatusCodes.Status201Created, "Product updated successfully!", typeof(ProductInventoryResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Product could not be updated...")]
    public async Task<IActionResult> UpdateProductInformation([FromForm] UpdateProductResource resource, [FromRoute] string productId)
    {
        var updateProductCommand = UpdateProductCommandFromResourceAssembler.ToCommandFromResource(resource, productId);
        var updatedProduct = await productCommandService.Handle(updateProductCommand);
        if (updatedProduct is null)
        {
            return BadRequest($"Failed to update product with ID {productId}. Please check the provided data.");
        }
        var updatedResource = ProductResourceFromEntityAssembler.ToResourceFromEntity(updatedProduct);
        return CreatedAtAction(nameof(GetProductById), new { productId = updatedResource.ProductId }, updatedResource);
    }

    /// <summary>
    /// This endpoint is used to delete a product by its unique identifier.
    /// </summary>
    /// <param name="productId">The unique identifier of the product to be deleted.</param>
    /// <returns>A response indicating the result of the deletion operation.</returns>
    [HttpDelete("{productId}")]
    [SwaggerOperation(
        Summary = "Delete a Product",
        Description = "Deletes a product by its unique identifier.",
        OperationId = "DeleteProduct"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "Product deleted successfully.")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Product not found.")]
    public async Task<IActionResult> DeleteProduct([FromRoute] string productId)
    {
        var deleteProductCommand = new DeleteProductCommand(productId);
        await productCommandService.Handle(deleteProductCommand);
        return Ok(new { Message = $"Product with ID {productId} has been deleted successfully." });
    }
}