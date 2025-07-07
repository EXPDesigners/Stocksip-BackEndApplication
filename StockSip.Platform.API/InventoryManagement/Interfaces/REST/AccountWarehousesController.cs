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
    /// This method creates a new warehouse associated with a specific Account ID.
    /// </summary>
    /// <param name="resource">The resource containing the warehouse details.</param>
    /// <param name="accountId">The unique identifier for the account with which the warehouse will be associated.</param>
    /// <returns>An IActionResult indicating the result of the operation.</returns>
    [HttpPost]
    [SwaggerOperation( 
        Summary = "Create a New Warehouse by Account ID",
        Description = "Creates a new warehouse associated with a specific Account ID.",
        OperationId = "CreateWarehouse")]
    [SwaggerResponse(StatusCodes.Status201Created, "Warehouse created successfully", typeof(WarehouseResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Warehouse could not be created")]    
    public async Task<IActionResult> CreateWarehouse([FromForm] CreateWarehouseResource resource, [FromRoute] string accountId)
    {
        var createWarehouseCommand = CreateWarehouseCommandFromResourceAssembler.ToCommandFromResource(resource, accountId);
        var warehouse = await warehouseCommandService.Handle(createWarehouseCommand);
        if (warehouse is null) return BadRequest("Failed to create warehouse. Please check the provided data.");
        var warehouseResource = WarehouseResourceFromEntityAssembler.ToResourceFromEntity(warehouse);
        return Ok(warehouseResource);
    }
    
    /// <summary>
    /// This method retrieves all warehouses associated with a specific Account ID.
    /// </summary>
    /// <param name="accountId">The unique identifier for the account whose warehouses are to be retrieved.</param>
    /// <returns>A list of warehouses associated with the specified Account ID.</returns>
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
        var warehouseList = warehouses.ToList();
        var resources = warehouseList
            .Select(WarehouseResourceFromEntityAssembler.ToResourceFromEntity)
            .ToList();
        return Ok(resources);
    }
    
    /// <summary>
    /// This method retrieves the count of warehouses associated with a specific Account ID.
    /// </summary>
    /// <param name="accountId">The unique identifier for the account whose warehouses count is to be retrieved.</param>
    /// <returns>A count of warehouses associated with the specified Account ID.</returns>
    [HttpGet("counts")]
    [SwaggerOperation(
        Summary = "Get Warehouses Count by Account ID",
        Description = "Retrieves the count of warehouses associated with a specific Account ID.",
        OperationId = "GetWarehousesCountByAccountId")]
    [SwaggerResponse(StatusCodes.Status200OK, "Count of warehouses found!", typeof(int))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "No warehouses found for the given Account ID.")]
    public async Task<IActionResult> GetWarehousesCountByAccountId([FromRoute] string accountId)
    {
        var getWarehousesCountByAccountIdQuery = new GetWarehousesCountUsagesQuery(accountId);
        var count = await warehouseQueryService.Handle(getWarehousesCountByAccountIdQuery);
        if (count < 0) return NotFound("No warehouses found for the given Account ID.");
        return Ok(new { Count = count });
    }
}