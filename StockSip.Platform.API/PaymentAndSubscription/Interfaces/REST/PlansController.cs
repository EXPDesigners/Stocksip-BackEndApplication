using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Queries;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Services;
using StockSip.Platform.API.PaymentAndSubscription.Interfaces.REST.Resources;
using StockSip.Platform.API.PaymentAndSubscription.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace StockSip.Platform.API.PaymentAndSubscription.Interfaces.REST;

/// <summary>
/// This controller provides endpoints for managing subscription plans.
/// </summary>
/// <param name="planQueryService">The query service for retrieving subscription plan information.</param>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Plans Endpoints")]
public class PlansController(IPlanQueryService planQueryService) : ControllerBase
{
    /// <summary>
    /// This endpoint retrieves all available subscription plans.
    /// </summary>
    /// <returns>A list of subscription plans.</returns>
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get All Plans",
        Description = "Retrieves all available subscription plans.",
        OperationId = "GetAllPlans")]
    [SwaggerResponse(StatusCodes.Status200OK, "Plans retrieved successfully", typeof(IEnumerable<PlanResource>))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "No plans found")]
    public async Task<IActionResult> GetAllPlans()
    {
        var plans = await planQueryService.Handle(new GetAllPlansQuery());
        var resource = PlansResourceFromEntityAssembler.ToResourceFromEntities(plans);
        return Ok(resource);
    }
}