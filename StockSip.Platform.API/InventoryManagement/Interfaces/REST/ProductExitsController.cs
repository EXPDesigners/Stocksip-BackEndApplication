using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Queries;
using StockSip.Platform.API.InventoryManagement.Domain.Services;
using StockSip.Platform.API.InventoryManagement.Interfaces.REST.Resources;
using StockSip.Platform.API.InventoryManagement.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace StockSip.Platform.API.InventoryManagement.Interfaces.REST;

/// <summary>
/// This controller provides endpoints for managing exits of a product.
/// </summary>
/// <param name="productQueryService">
/// The query service for retrieving product information, used to validate product exits.
/// </param>
[ApiController]
[Route("api/v1/products/{productId}/exits")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Products")]
public class ProductExitsController(
    IProductQueryService productQueryService) : ControllerBase
{
    /// <summary>
    /// This endpoint retrieves all product exits associated with a specific product ID.
    /// </summary>
    /// <param name="productId">
    /// The ID of the product for which exits are being queried.
    /// </param>
    /// <returns>
    /// A list of product exits associated with the specified product ID.
    /// </returns>
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all product exits by product ID",
        Description = "Retrieves all product exits associated with a specific product ID.",
        OperationId = "GetProductExitsByProductId")]
    [SwaggerResponse(200, "Returns all product exits by product ID.", typeof(ProductExitResource))]
    [SwaggerResponse(404, "No product exits found for the specified product.")]
    public async Task<IActionResult> GetProductExitsByProductId(string productId)
    {
        var getAllProductExitsByProductIdQuery = new GetAllProductExitsByProductIdQuery(productId);
        var productExits = await productQueryService.Handle(getAllProductExitsByProductIdQuery);
        var enumerable = productExits.ToList();
        if (enumerable.Count == 0)
        {
            return NotFound(new { Message = "No product exits found for the specified product." });
        }
        var productExitResource = enumerable.Select(ProductExitResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(productExitResource);
    }
}