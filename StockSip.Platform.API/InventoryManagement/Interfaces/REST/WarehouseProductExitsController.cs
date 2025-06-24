using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Queries;
using StockSip.Platform.API.InventoryManagement.Domain.Services;
using StockSip.Platform.API.InventoryManagement.Interfaces.REST.Resources;
using StockSip.Platform.API.InventoryManagement.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace StockSip.Platform.API.InventoryManagement.Interfaces.REST;

/// <summary>
/// This controller provides endpoints for managing product exits from warehouses.
/// </summary>
/// <param name="warehouseCommandService">
/// The command service for handling warehouse operations, specifically for product exits.
/// </param>
/// <param name="productQueryService">
/// The query service for retrieving product information, used to validate product exits.
/// </param>
/// <param name="warehouseQueryService">
/// The query service for retrieving warehouse information, used to validate warehouse exits.
/// </param>
[ApiController]
[Route("api/v1/warehouses/{warehouseId}/product-exits")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Warehouses")]
public class WarehouseProductExitsController(
    IWarehouseCommandService warehouseCommandService,
    IProductQueryService productQueryService,
    IWarehouseQueryService warehouseQueryService) : ControllerBase
{
    /// <summary>
    /// This endpoint registers a product exit from a specific warehouse for a given product.
    /// </summary>
    /// <param name="resource">
    /// The resource containing the details of the product exit to be registered.
    /// </param>
    /// <param name="warehouseId">
    /// The ID of the warehouse from which the product is exiting.
    /// </param>
    /// <param name="productId">
    /// The ID of the product that is exiting the warehouse.
    /// </param>
    /// <returns>
    /// The registered product exit resource if successful, or a NotFound result if the product or warehouse does not exist.
    /// </returns>
    [HttpPost("{productId}")]
    [SwaggerOperation(
        Summary = "Register a product exit",
        Description = "Registers a product exit from a specific warehouse for a given product.",
        OperationId = "RegisterProductExit")]
    [SwaggerResponse(StatusCodes.Status200OK, "Product exit registered successfully.", typeof(ProductExitResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request data...")]
    public async Task<IActionResult> RegisterProductExit(
        [FromBody] RegisterProductExitResource resource,
        string warehouseId,
        string productId)
    {
        var registerProductExitCommand =
            RegisterProductExitCommandFromResourceAssembler.ToCommandFromResource(resource, warehouseId, productId);
        var registeredProductExit = await warehouseCommandService.Handle(registerProductExitCommand);
        if (registeredProductExit == null)
        {
            return NotFound(new { Message = "Product exit could not be registered." });
        }
        var productExitResource = ProductExitResourceFromEntityAssembler.ToResourceFromEntity(registeredProductExit);
        return CreatedAtAction(
            nameof(GetProductExitsByProductIdAndWarehouseId),
            new { warehouseId, productId },
            productExitResource);
    }
    
    /// <summary>
    /// This endpoint retrieves all product exits associated with a specific warehouse ID.
    /// </summary>
    /// <param name="warehouseId">
    /// The ID of the warehouse for which product exits are being queried.
    /// </param>
    /// <returns>
    /// The list of product exits associated with the specified warehouse ID.
    /// </returns>
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all product exits by warehouse ID",
        Description = "Retrieves all product exits associated with a specific warehouse ID.",
        OperationId = "GetProductExitsByWarehouseId"
    )]
    [SwaggerResponse(200, "Returns all product exits by warehouse ID.", typeof(ProductExitResource))]
    [SwaggerResponse(404, "No product exits found for the specified warehouse.")]
    public async Task<IActionResult> GetProductExitsByWarehouseId(string warehouseId)
    {
        var getAllProductExitsByWarehouseIdQuery = new GetAllProductExitsByWarehouseIdQuery(warehouseId);
        var productExits = await warehouseQueryService.Handle(getAllProductExitsByWarehouseIdQuery);
        var enumerable = productExits.ToList();
        if (enumerable.Count == 0)
        {
            return NotFound(new { Message = "No product exits found for the specified warehouse." });
        }
        var productExitResource = enumerable.Select(ProductExitResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(productExitResource);
    }

    /// <summary>
    /// This endpoint retrieves product exits by warehouse ID and product ID.
    /// </summary>
    /// <param name="warehouseId">
    /// The ID of the warehouse from which the product exits are being queried.
    /// </param>
    /// <param name="productId">
    /// The ID of the product for which exits are being queried.
    /// </param>
    /// <returns>
    /// A list of product exits associated with the specified warehouse ID and product ID.
    /// </returns>
    [HttpGet("{productId}")]
    [SwaggerOperation(
        Summary = "Get product exits by warehouse ID and product ID",
        Description = "Retrieves all product exits associated with a specific warehouse ID and product ID.",
        OperationId = "GetProductExitsByProductIdAndWarehouseId")]
    [SwaggerResponse(200, "Returns product exits by warehouse ID and product ID.", typeof(ProductExitResource))]
    [SwaggerResponse(404, "No product exits found for the specified product and warehouse.")]
    public async Task<IActionResult> GetProductExitsByProductIdAndWarehouseId(string warehouseId, string productId)
    {
        var getAllProductExitsByProductIdAndWarehouseIdQuery = 
            new GetAllProductExitsByProductIdAndWarehouseIdQuery(warehouseId, productId);
        var productExits = await warehouseQueryService.Handle(getAllProductExitsByProductIdAndWarehouseIdQuery);
        var enumerable = productExits.ToList();
        if (enumerable.Count == 0)
        {
            return NotFound(new { Message = "No product exits found for the specified product and warehouse." });
        }
        var productExitResource = enumerable.Select(ProductExitResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(productExitResource);
    }
}