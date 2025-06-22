using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Queries;
using StockSip.Platform.API.InventoryManagement.Domain.Services;
using StockSip.Platform.API.InventoryManagement.Interfaces.REST.Resources;
using StockSip.Platform.API.InventoryManagement.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace StockSip.Platform.API.InventoryManagement.Interfaces.REST;

[ApiController]
[Route("api/v1/products/{productId}/care-guide")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Products")]
public class ProductCareGuidesController (
    ICareGuideQueryService careGuideQueryService
    ) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get Care Guide by Product ID",
        Description = "Retrieves a care guide by its associated Product ID.",
        OperationId = "GetCareGuideByProductId")]
    [SwaggerResponse(StatusCodes.Status200OK, "Care Guide found!", typeof(CareGuideResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Care Guide not found...")]
    public async Task<IActionResult> GetCareGuideByProductId([FromRoute] string productId)
    {
        var getCareGuideByProductIdQuery = new GetCareGuideByProductIdQuery(productId);
        var careGuide = await careGuideQueryService.Handle(getCareGuideByProductIdQuery);
        if (careGuide is null)
        {
            return NotFound($"Care guide associated with Product ID {productId} not found...");
        }
        var resource = CareGuideResourceFromEntityAssembler.ToResourceFromEntity(careGuide);
        return Ok(resource);
    } 
}