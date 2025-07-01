using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Queries;
using StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;
using StockSip.Platform.API.InventoryManagement.Domain.Services;
using StockSip.Platform.API.InventoryManagement.Interfaces.REST.Resources;
using StockSip.Platform.API.InventoryManagement.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace StockSip.Platform.API.InventoryManagement.Interfaces.REST;

[ApiController]
[Route("api/v1/accounts/{accountId}/products")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Accounts")]
public class AccountProductsController (
    IProductQueryService productQueryService,
    IProductCommandService productCommandService
    ) : ControllerBase
{
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
    [SwaggerResponse(StatusCodes.Status201Created, "Product created successfully!", typeof(ProductInventoryResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Product could not be created...")]
    public async Task<IActionResult> CreateProduct([FromForm] CreateProductResource resource, [FromRoute] string accountId)
    {
        var createProductCommand = CreateProductCommandFromResourceAssembler.ToCommandFromResource(resource, accountId);
        var product = await productCommandService.Handle(createProductCommand);
        if (product is null) return BadRequest("Failed to create product resource.");
        var productResource = ProductResourceFromEntityAssembler.ToResourceFromEntity(product);
        return Ok(productResource);
    }
    
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Products by Account ID",
        Description = "Retrieves a list of products by a specific Account ID.",
        OperationId = "GetAllProductsByAccountId")]
    [SwaggerResponse(StatusCodes.Status200OK, "Products found!", typeof(ProductInventoryResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Products not found for the give Account ID...")]
    public async Task<IActionResult> GetAllProductsByAccountId([FromRoute] string accountId)
    {
        var targetAccountId = new AccountId(accountId);
        var getAllProductsByAccountIdQuery = new GetAllProductsByAccountIdQuery(targetAccountId);
        var products = await productQueryService.Handle(getAllProductsByAccountIdQuery);
        var enumerable = products.ToList();
        if (enumerable.Count == 0)
        {
            return NotFound($"Products for Account ID {accountId} not found...");
        }

        var resources = enumerable.Select(ProductResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
}