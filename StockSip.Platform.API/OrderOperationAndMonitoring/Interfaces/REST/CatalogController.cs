using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.Commands;
using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.Queries;
using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.ValueObjects;
using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Services;
using StockSip.Platform.API.OrderOperationAndMonitoring.Interfaces.REST.Resources;
using StockSip.Platform.API.OrderOperationAndMonitoring.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace StockSip.Platform.API.OrderOperationAndMonitoring.Interfaces.REST;

[ApiController]
[Route("api/v1/catalogs")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Catalog management endpoints")]
public class CatalogsController(
    ICatalogCommandService catalogCommandService,
    ICatalogQueryService catalogQueryService
) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(Summary = "Get catalogs", Description = "Filter by accountId and/or isPublished")]
    public async Task<IActionResult> GetCatalogs([FromQuery] string? accountId, [FromQuery] bool? isPublished)
    {
        IEnumerable<Domain.Model.Aggregates.Catalog> catalogs;

        if (!string.IsNullOrEmpty(accountId) && isPublished == true)
            catalogs = await catalogQueryService.Handle(new GetPublishedCatalogsByAccountIdQuery(new AccountId(accountId), OnlyPublished: true));
        else if (isPublished == true)
            catalogs = await catalogQueryService.Handle(new GetPublishedCatalogsQuery(OnlyPublished: true));
        else if (!string.IsNullOrEmpty(accountId))
            catalogs = await catalogQueryService.Handle(new GetCatalogsByAccountQuery(new AccountId(accountId)));
        else
            catalogs = await catalogQueryService.Handle(new GetAllCatalogsQuery());

        if (!catalogs.Any()) return NotFound();

        return Ok(catalogs.Select(CatalogResourceFromEntityAssembler.ToResourceFromEntity));
    }

    [HttpGet("{catalogId}")]
    [SwaggerOperation(Summary = "Get catalog by ID")]
    public async Task<IActionResult> GetCatalogById([FromRoute] long catalogId)
    {
        var catalog = await catalogQueryService.Handle(new GetCatalogByIdQuery(catalogId));
        return catalog is null
            ? NotFound()
            : Ok(CatalogResourceFromEntityAssembler.ToResourceFromEntity(catalog));
    }

    [HttpGet("published")]
    [SwaggerOperation(Summary = "Get published catalogs by provider email")]
    public async Task<IActionResult> GetPublishedByProviderEmail([FromQuery] string providerEmail)
    {
        try
        {
            var catalogs = await catalogQueryService.GetPublishedCatalogsByProviderEmailAsync(providerEmail);
            return Ok(catalogs.Select(CatalogResourceFromEntityAssembler.ToResourceFromEntity));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [SwaggerOperation(Summary = "Create catalog")]
    [SwaggerResponse(StatusCodes.Status201Created, "Catalog created")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Validation error")]
    public async Task<IActionResult> CreateCatalog([FromBody] CreateCatalogResource resource)
    {
        var command = CreateCatalogCommandFromResourceAssembler.ToCommandFromResource(resource);
        var catalog = await catalogCommandService.Handle(command);
        var result = CatalogResourceFromEntityAssembler.ToResourceFromEntity(catalog);

        return Created($"/api/v1/catalogs/{catalog.CatalogId}", result);
    }

    [HttpPut("{catalogId}")]
    [SwaggerOperation(Summary = "Update catalog (name only)")]
    public async Task<IActionResult> UpdateCatalog([FromRoute] long catalogId, [FromBody] UpdateCatalogResource resource)
    {
        var command = UpdateCatalogCommandFromResourceAssembler.ToCommandFromResource(catalogId, resource);
        var updated = await catalogCommandService.Handle(command);
        return updated is null
            ? NotFound()
            : Ok(CatalogResourceFromEntityAssembler.ToResourceFromEntity(updated));
    }

    [HttpPost("{catalogId}/publish")]
    [SwaggerOperation(Summary = "Publish catalog")]
    public async Task<IActionResult> PublishCatalog([FromRoute] long catalogId)
    {
        var command = new PublishCatalogCommand(catalogId);
        var published = await catalogCommandService.Handle(command);
        return published is null
            ? NotFound()
            : Ok(CatalogResourceFromEntityAssembler.ToResourceFromEntity(published));
    }
    
    [HttpGet("catalogItems")]
    [SwaggerOperation(Summary = "Get catalog items")]
    public async Task<IActionResult> GetCatalogItems([FromQuery] long catalogId)
    {
        var items = await catalogQueryService
            .Handle(new GetCatalogItemsByCatalogIdQuery(catalogId));
        if (!items.Any()) return NotFound();

        var resources = items
            .Select(CatalogItemResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
    
    [HttpPost("catalogItems")]
    [SwaggerOperation(Summary = "Create catalog item")]
    [SwaggerResponse(StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateCatalogItem(
        [FromBody] CreateCatalogItemResource body)   // ← recurso correcto
    {
        var cmd     = CreateCatalogItemCommandFromResourceAssembler.ToCommandFromResource(body);
        var created = await catalogCommandService.Handle(cmd);           // método async

        if (created is null) 
            return NotFound("Catalog not found.");

        var resource = CatalogItemResourceFromEntityAssembler.ToResourceFromEntity(created);
        return Created($"/api/v1/catalogItems/{created.Id}", resource);
    }


    [HttpDelete("catalogItems/{id}")]
    [SwaggerOperation(Summary = "Delete catalog item")]
    public async Task<IActionResult> DeleteCatalogItem([FromRoute] string id)
    {
        var success = await catalogCommandService
            .Handle(new DeleteCatalogItemCommand(id));
        return success ? NoContent() : NotFound();
    }
}
