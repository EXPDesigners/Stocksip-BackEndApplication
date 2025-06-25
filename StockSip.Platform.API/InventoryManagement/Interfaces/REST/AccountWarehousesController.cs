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
    IWarehouseQueryService warehouseQueryService,
    IWarehouseCommandService warehouseCommandService
    ) : ControllerBase
{
    
    /// <summary>
    /// This endpoint creates a new warehouse.
    /// </summary>
    /// <param name="resource">The resource containing the warehouse details to be created.</param>
    /// <returns>An IActionResult indicating the result of the creation operation.</returns>
    [HttpPost]
    [SwaggerOperation( 
        Summary = "Create a New Warehouse by Account ID",
        Description = "Creates a new warehouse associated with a specific Account ID.",
        OperationId = "CreateWarehouse")]
    [SwaggerResponse(StatusCodes.Status201Created, "Warehouse created successfully", typeof(WarehouseResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Warehouse could not be created")]    
    public async Task<IActionResult> CreateWarehouse([FromBody] CreateWarehouseResource resource, [FromRoute] string accountId)
    {
        var createWarehouseCommand = CreateWarehouseCommandFromResourceAssembler.ToCommandFromResource(resource, accountId);
        var warehouse = await warehouseCommandService.Handle(createWarehouseCommand);
        if (warehouse is null) return BadRequest("Failed to create warehouse. Please check the provided data.");
        var warehouseResource = WarehouseResourceFromEntityAssembler.ToResourceFromEntity(warehouse);
        return Ok(warehouseResource);
    }
    
    
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