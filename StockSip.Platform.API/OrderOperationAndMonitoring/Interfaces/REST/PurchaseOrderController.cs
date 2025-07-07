using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.ValueObjects;
using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Services;
using StockSip.Platform.API.OrderOperationAndMonitoring.Interfaces.REST.Resources;
using StockSip.Platform.API.OrderOperationAndMonitoring.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace StockSip.Platform.API.OrderOperationAndMonitoring.Interfaces.REST;

[ApiController]
[Route("api/v1/orders")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Order management endpoints")]
public class PurchaseOrdersController(
    IPurchaseOrderCommandService commandService,
    IPurchaseOrderQueryService  queryService)
    : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(Summary = "Create order")]
    [SwaggerResponse(StatusCodes.Status201Created, "Order created", typeof(PurchaseOrderResource))]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderResource body)
    {
        var cmd      = CreateOrderCommandFromResourceAssembler.ToCommandFromResource(body);
        var created  = await commandService.HandleAsync(cmd);

        var resource = PurchaseOrderResourceFromEntityAssembler.ToResourceFromEntity(created);

        return Created($"/api/v1/orders/{created.Id}", resource);
    }
    
    [HttpPatch("{id:long}/status")]
    [SwaggerOperation(Summary = "Change order status")]
    [SwaggerResponse(StatusCodes.Status204NoContent, "Status updated")]
    public async Task<IActionResult> ChangeStatus(
        [FromRoute] long id,
        [FromQuery] OrderStatus status)
    {
        await commandService.ChangeStatusAsync(id, status);
        return NoContent();
    }

    [HttpGet]
    [SwaggerOperation(Summary = "Get all orders")]
    public async Task<ActionResult<IEnumerable<PurchaseOrderResource>>> GetAll()
    {
        var orders = await queryService.FindAllAsync();
        var res    = orders.Select(PurchaseOrderResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(res);
    }

    [HttpGet("buyer/{buyerId}")]
    [SwaggerOperation(Summary = "Get orders by buyer account")]
    public async Task<ActionResult<IEnumerable<PurchaseOrderResource>>> GetByBuyer([FromRoute] string buyerId)
    {
        var orders = await queryService.FindByBuyerAccountIdAsync(buyerId);
        var res    = orders.Select(PurchaseOrderResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(res);
    }

    [HttpGet("supplier/{supplierId}")]
    [SwaggerOperation(Summary = "Get orders by supplier account")]
    public async Task<ActionResult<IEnumerable<PurchaseOrderResource>>> GetBySupplier([FromRoute] string supplierId)
    {
        var orders = await queryService.FindBySupplierAccountIdAsync(supplierId);
        var res    = orders.Select(PurchaseOrderResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(res);
    }
}