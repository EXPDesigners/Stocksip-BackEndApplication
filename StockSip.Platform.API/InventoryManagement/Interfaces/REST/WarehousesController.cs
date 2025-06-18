using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Queries;
using StockSip.Platform.API.InventoryManagement.Domain.Services;
using StockSip.Platform.API.InventoryManagement.Interfaces.REST.Resources;
using StockSip.Platform.API.InventoryManagement.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace StockSip.Platform.API.InventoryManagement.Interfaces.REST;

/// <summary>
/// This controller provides endpoints for managing warehouses.
/// </summary>
/// <param name="warehouseCommandService">The command service for handling warehouse operations.</param>
/// <param name="warehouseQueryService">The query service for retrieving warehouse information.</param>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Warehouse Endpoints")]
public class WarehousesController(IWarehouseCommandService warehouseCommandService, IWarehouseQueryService warehouseQueryService) : ControllerBase
{
    /// <summary>
    /// This endpoint creates a new warehouse.
    /// </summary>
    /// <param name="resource">The resource containing the warehouse details to be created.</param>
    /// <returns>An IActionResult indicating the result of the creation operation.</returns>
    [HttpPost]
    [SwaggerOperation( 
        Summary = "Create a New Warehouse",
        Description = "Creates a new Warehouse and returns the created warehouse resource.",
        OperationId = "CreateWarehouse")]
    [SwaggerResponse(StatusCodes.Status201Created, "Warehouse created successfully", typeof(WarehouseResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Warehouse could not be created")]    
    public async Task<IActionResult> CreateWarehouse([FromBody] CreateWarehouseResource resource)
    {
        var createWarehouseCommand = CreateWarehouseCommandFromResourceAssembler.ToCommandFromResource(resource);
        var warehouse = await warehouseCommandService.Handle(createWarehouseCommand);
        if (warehouse is null) return BadRequest("Failed to create warehouse. Please check the provided data.");
        var createdResource = WarehouseResourceFromEntityAssembler.ToResourceFromEntity(warehouse);
        return CreatedAtAction(nameof(GetWarehouseById), new { warehouseId = warehouse.WarehouseId }, createdResource);
    }
    
        
    [HttpPut("{warehouseId:int}")]
    [SwaggerOperation(
        Summary = "Update an Existing Warehouse",
        Description = "Update the information of an existing warehouse.",
        OperationId = "UpdateWarehouse")]
    [SwaggerResponse(StatusCodes.Status201Created, "Warehouse updated successfully", typeof(WarehouseResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Warehouse could not be updated")]
    public async Task<IActionResult> UpdateWarehouse([FromRoute] string warehouseId, [FromBody] UpdateWarehouseResource resource)
    {
        var createWarehouseCommand = UpdateWarehouseCommandFromResourceAssembler.ToCommandFromResource(resource, warehouseId);
        var warehouse = await warehouseCommandService.Handle(createWarehouseCommand);
        if (warehouse is null) return BadRequest("Failed to update warehouse. Please check the provided data.");
        var updatedResource = WarehouseResourceFromEntityAssembler.ToResourceFromEntity(warehouse);
        return CreatedAtAction(nameof(GetWarehouseById), new { warehouseId = warehouse.WarehouseId }, updatedResource);
    }

    /// <summary>
    /// This endpoint retrieves a warehouse by its unique identifier.
    /// </summary>
    /// <param name="warehouseId">The unique identifier of the warehouse to retrieve.</param>
    /// <returns>An IActionResult containing the warehouse resource if found, or a NotFound result if not found.</returns>
    [HttpGet("{warehouseId:int}")]
    [SwaggerOperation( 
        Summary = "Get Warehouse by Id",
        Description = "Returns a warehouse by its unique identifier.",
        OperationId = "GetWarehouseById")]
    [SwaggerResponse(StatusCodes.Status200OK, "Warehouse found", typeof(WarehouseResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Warehouse not found")]
    public async Task<IActionResult> GetWarehouseById([FromRoute] string warehouseId)
    {
        var warehouse = await warehouseQueryService.Handle(new GetWarehouseByIdQuery(warehouseId));
        if (warehouse is null) return NotFound($"Warehouse with ID {warehouseId} not found.");
        var resource = WarehouseResourceFromEntityAssembler.ToResourceFromEntity(warehouse);
        return Ok(resource);
    }
}