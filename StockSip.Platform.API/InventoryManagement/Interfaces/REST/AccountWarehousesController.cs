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
[Route("api/v1/accounts/{accountId}/warehouses")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Accounts")]
public class AccountWarehousesController (
    IWarehouseQueryService warehouseQueryService
    ) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Warehouses by Account ID",
        Description = "Retrieves a list of warehouses by a specific Account ID.",
        OperationId = "GetAllWarehousesByAccountId")]
    [SwaggerResponse(StatusCodes.Status200OK, "Warehouses found!", typeof(WarehouseResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Warehouses not found for the give Account ID...")]
    public async Task<IActionResult> GetAllWarehousesByAccountId([FromRoute] string accountId)
    {
        var targetAccountId = new AccountId(accountId);
        var getAllWarehousesByAccountIdQuery = new GetAllWarehousesByAccountIdQuery(targetAccountId);
        var warehouses = await warehouseQueryService.Handle(getAllWarehousesByAccountIdQuery);
        var enumerable = warehouses.ToList();
        if (enumerable.Count == 0)
        {
            return NotFound($"Warehouses for Account ID {accountId} not found...");
        }

        var resources = enumerable.Select(WarehouseResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
}