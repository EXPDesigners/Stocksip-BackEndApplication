using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
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
    [SwaggerResponse(StatusCodes.Status200OK, "Product found!", typeof(ProductResource))]
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
    /// This endpoint creates a new product.
    /// </summary>
    /// <param name="resource">
    /// The resource containing the product details to be created.
    /// </param>
    /// <returns>
    /// An IActionResult indicating the result of the creation operation, including the created product resource if successful.
    /// </returns>
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a New Product",
        Description = "Creates a new product and returns the created product resource.",
        OperationId = "CreateProduct")]
    [SwaggerResponse(StatusCodes.Status201Created, "Product created successfully!", typeof(ProductResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Product could not be created...")]
    public async Task<IActionResult> CreateProduct([FromBody] CreateProductResource resource)
    {
        var createProductCommand = CreateProductCommandFromResourceAssembler.ToCommandFromResource(resource);
        var product = await productCommandService.Handle(createProductCommand);
        if (product is null)
        {
            return BadRequest("Failed to create product. Please check the provided data...");
        }
        var createdResource = ProductResourceFromEntityAssembler.ToResourceFromEntity(product);
        return CreatedAtAction(nameof(GetProductById), new { productId = createdResource.Id }, createdResource);
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
    [SwaggerResponse(StatusCodes.Status201Created, "Product updated successfully!", typeof(ProductResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Product could not be updated...")]
    public async Task<IActionResult> UpdateProductInformation([FromBody] UpdateProductResource resource, [FromRoute] string productId)
    {
        var updateProductCommand = UpdateProductCommandFromResourceAssembler.ToCommandFromResource(resource, productId);
        var updatedProduct = await productCommandService.Handle(updateProductCommand);
        if (updatedProduct is null)
        {
            return BadRequest($"Failed to update product with ID {productId}. Please check the provided data.");
        }
        var updatedResource = ProductResourceFromEntityAssembler.ToResourceFromEntity(updatedProduct);
        return CreatedAtAction(nameof(GetProductById), new { productId = updatedResource.Id }, updatedResource);
    }

    /// <summary>
    /// This endpoint retrieves all products associated with a specific profile ID.
    /// </summary>
    /// <param name="profileId">
    /// The unique identifier of the profile for which products are to be retrieved.
    /// </param>
    /// <returns>
    /// An IActionResult containing a list of product resources if found, or a NotFound result if no products are found for the specified profile ID.
    /// </returns>
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all products by profile ID",
        Description = "Retrieves all products associated with a specific profile ID.",
        OperationId = "GetAllProductsByProfileId")]
    [SwaggerResponse(StatusCodes.Status200OK, "Products found!", typeof(IEnumerable<ProductResource>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "No products found for the specified profile ID...")]
    public async Task<IActionResult> GetAllProductsByProfileId(string profileId)
    {
        var targetProfileId = new AccountId(profileId);
        var getAllProductsByProfileIdQuery = new GetAllProductsByAccountIdQuery(targetProfileId);
        var products = await productQueryService.Handle(getAllProductsByProfileIdQuery);
        var productsEnumerable = products.ToList();
        if (productsEnumerable.Count == 0)
        {
            return NotFound($"No products found for profile with ID {profileId}.");
        }
        var productResources = productsEnumerable.Select(ProductResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(productResources);
    }
}