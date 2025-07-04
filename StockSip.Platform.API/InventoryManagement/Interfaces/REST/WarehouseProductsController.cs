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
/// This controller provides endpoints for managing warehouse products.
/// </summary>
/// <param name="productCommandService">The command service for handling product operations.</param>
/// <param name="productQueryService">The query service for retrieving product information.</param>
[ApiController]
[Route("api/v1/warehouses/{warehouseId}/products")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Warehouses")]
public class WarehouseProductsController(
    IProductCommandService productCommandService,
    IProductQueryService productQueryService) : ControllerBase
{
    
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
    [HttpGet("provider/{providerId}")]
    [SwaggerOperation(
        Summary = "Get all products by provider and warehouse ID",
        Description = "Retrieves all products with a specific Provider and Warehouse ID.",
        OperationId = "GetAllProductsByProviderIdAndWarehouseId")]
    [SwaggerResponse(StatusCodes.Status200OK, "List of products found!", typeof(IEnumerable<ProductInventoryResource>))]
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
}