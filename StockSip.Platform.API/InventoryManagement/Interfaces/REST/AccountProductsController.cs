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
    IProductQueryService productQueryService
    ) : ControllerBase
{
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