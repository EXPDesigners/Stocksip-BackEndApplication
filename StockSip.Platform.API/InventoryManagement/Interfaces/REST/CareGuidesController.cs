using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Queries;
using StockSip.Platform.API.InventoryManagement.Domain.Services;
using StockSip.Platform.API.InventoryManagement.Interfaces.REST.Resources;
using StockSip.Platform.API.InventoryManagement.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace StockSip.Platform.API.InventoryManagement.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Care Guide Endpoints")]
public class CareGuidesController(
    ICareGuideCommandService careGuideCommandService,
    ICareGuideQueryService careGuideQueryService
    ) : ControllerBase
{
    [HttpGet("{careGuideId}")]
    [SwaggerOperation(
        Summary = "Get Care Guide by ID",
        Description = "Retrieves a care guide by its unique identifier.",
        OperationId = "GetCareGuideById")]
    [SwaggerResponse(StatusCodes.Status200OK, "Care Guide found!", typeof(CareGuideResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Care Guide not found...")]
    public async Task<IActionResult> GetCareGuideById([FromRoute] string accountId, [FromRoute] string careGuideId)
    {
        var getCareGuideByIdQuery = new GetCareGuideByIdQuery(careGuideId);
        var careGuide = await careGuideQueryService.Handle(getCareGuideByIdQuery);
        if (careGuide == null)
        {
            return NotFound($"Care guide with ID {careGuideId} not found...");
        }
        var resource = CareGuideResourceFromEntityAssembler.ToResourceFromEntity(careGuide);
        return Ok(resource);
    }
    
    [HttpPut("{careGuideId}")]
    [SwaggerOperation(
        Summary = "Update Care Guide",
        Description = "Update Recommendations for A Specific Care Guide.",
        OperationId = "UpdateCareGuide")]
    [SwaggerResponse(StatusCodes.Status200OK, "Care Guide updated successfully!", typeof(CareGuideResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Care Guide could not be updated...")]
    public async Task<IActionResult> UpdateCareGuideRecommendations([FromBody] UpdateCareGuideResource resource, [FromRoute] string careGuideId)
    {
        var updateCareGuideCommand = UpdateCareGuideCommandFromResourceAssembler.ToCommandFromResource(resource, careGuideId);
        var updatedCareGuide = await careGuideCommandService.Handle(updateCareGuideCommand);
        if (updatedCareGuide is null)
        {
            return BadRequest($"Failed to update care guide with ID {careGuideId}. Please check the provided data.");
        }
        var updatedCareGuideResource = CareGuideResourceFromEntityAssembler.ToResourceFromEntity(updatedCareGuide);
        return Ok(updatedCareGuideResource);
    }

    [HttpDelete]
    [SwaggerOperation(
        Summary = "Delete a Care Guide",
        Description = "Delete a Specific Care Guide.",
        OperationId = "DeleteCareGuide")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Care Guide deleted successfully!")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Care Guide not found...")]
    public async Task<IActionResult> DeleteCareGuide([FromRoute] string careGuideId)
    {
        var deleteCareGuideCommand = new DeleteCareGuideCommand(careGuideId);
        await careGuideCommandService.Handle(deleteCareGuideCommand);
        return Ok(new {Message = $"Care Guide with ID {careGuideId} deleted successfully."});
    }

    [HttpPut("{careGuideId}/deallocations")]
    [SwaggerOperation(
        Summary = "Unassing a Care Guide",
        Description = "Unassign a Care Guide From Its Current Product.",
        OperationId = "DeallocateCareGuide")]
    [SwaggerResponse(StatusCodes.Status200OK, "Care Guide unassigned successfully!")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Care Guide could not be unassign...")]
    public async Task<IActionResult> DeallocateCareGuide([FromRoute] string careGuideId)
    {
        var unassignCareGuideCommand = new UnassignCareGuideCommand(careGuideId);
        await careGuideCommandService.Handle(unassignCareGuideCommand);
        return Ok(new
            { Message = $"Care Guide with ID {careGuideId} unassigned from its current product successfully." });
    }

    [HttpPut("{careGuideId}/allocations/{productId}")]
    [SwaggerOperation(
        Summary = "Assign a Care Guide",
        Description = "Assign a Care Guide To a Specific Product.",
        OperationId = "AllocateCareGuideToProduct")]
    [SwaggerResponse(StatusCodes.Status200OK, "Care Guide assigned successfully!")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Care Guide could not be assigned...")]
    public async Task<IActionResult> AllocateCareGuideToProduct([FromRoute] string careGuideId, [FromRoute] string productId)
    {
        var assignCareGuideCommand = new AssignCareGuideToProductCommand(careGuideId, productId);
        await careGuideCommandService.Handle(assignCareGuideCommand);
        return Ok(new
            { Message = $"Care Guide with ID {careGuideId} assigned to product with ID {productId} successfully." });
    }
}