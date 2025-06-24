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
[Route("api/v1/accounts/{accountId}/care-guides")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Accounts")]
public class AccountCareGuidesController (
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

    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Care Guides by Account ID",
        Description = "Retrieves a list of care guides by a specific Account ID.",
        OperationId = "GetAllCareGuidesByAccountId")]
    [SwaggerResponse(StatusCodes.Status200OK, "Care Guides found!", typeof(CareGuideResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Care Guides not found for the give Account ID...")]
    public async Task<IActionResult> GetAllCareGuidesByAccountId([FromRoute] string accountId)
    {
        var targetAccountId = new AccountId(accountId);
        var getAllCareGuidesByAccountIdCommand = new GetAllCareGuidesByAccountId(targetAccountId);
        var careGuides = await careGuideQueryService.Handle(getAllCareGuidesByAccountIdCommand);
        var enumerable = careGuides.ToList();
        if (enumerable.Count == 0)
        {
            return NotFound($"Care guides for Account ID {accountId} not found...");
        }
        var resources = enumerable.Select(CareGuideResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a Care Guide Without a Product.",
        Description = "Create a Care Guide without assigning it to a specific product.",
        OperationId = "CreateCareGuideWithoutProduct")]
    [SwaggerResponse(StatusCodes.Status201Created, "Care Guide created!", typeof(CareGuideResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Care Guide could not be created...")]
    public async Task<IActionResult> CreateCareGuideWithoutProduct(
        [FromBody] CreateCareGuideWithoutProductIdResource resource, 
        [FromRoute] string accountId)
    {
        var createCareGuideWithoutProductCommand = CreateCareGuideWithoutProductIdCommandFromResourceAssembler.ToCommandFromResource(resource, accountId);
        var careGuideToCreate = await careGuideCommandService.Handle(createCareGuideWithoutProductCommand);
        if (careGuideToCreate is null)
        {
            return BadRequest("Failed to create care guide. Please check the provided data.");
        }
        var createdCareGuide = CareGuideResourceFromEntityAssembler.ToResourceFromEntity(careGuideToCreate);
        return CreatedAtAction(nameof(GetCareGuideById), new { careGuideId = createdCareGuide.Id }, createdCareGuide);
    }
    
    [HttpPost("product/{productId}")]
    [SwaggerOperation(
        Summary = "Create a Care Guide.",
        Description = "Create a Care Guide And Assign it to a specific product.",
        OperationId = "CreateCareGuide")]
    [SwaggerResponse(StatusCodes.Status201Created, "Care Guide created and assigned!", typeof(CareGuideResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Care Guide could not be created...")]
    public async Task<IActionResult> CreateCareGuide(
        [FromBody] CreateCareGuideResource resource,
        [FromRoute] string accountId,
        [FromRoute] string productId)
    {
        var createCareGuideCommand = CreateCareGuideCommandFromResourceAssembler.ToCommandFromResource(resource, accountId, productId);
        var careGuideToCreate = await careGuideCommandService.Handle(createCareGuideCommand);
        if (careGuideToCreate is null)
        {
            return BadRequest("Failed to create care guide. Please check the provided data.");
        }
        var createdResource = CareGuideResourceFromEntityAssembler.ToResourceFromEntity(careGuideToCreate);
        return CreatedAtAction(nameof(GetCareGuideById), new { careGuideId = createdResource.Id }, createdResource);
    }
}