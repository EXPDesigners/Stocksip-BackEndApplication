using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using StockSip.Platform.API.AlertsAndNotifications.Domain.Model.Queries;
using StockSip.Platform.API.AlertsAndNotifications.Domain.Services;
using StockSip.Platform.API.AlertsAndNotifications.Interfaces.REST.Resources;
using StockSip.Platform.API.AlertsAndNotifications.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace StockSip.Platform.API.AlertsAndNotifications.Interfaces.REST;

/// <summary>
/// This controller provides endpoints for managing alerts.
/// </summary>
/// <param name="alertCommandService">
/// The command service for handling alert operations.
/// </param>
/// <param name="alertQueryService">
/// The query service for retrieving alert information.
/// </param>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Alert Endpoints")]
public class AlertsController(
    IAlertCommandService alertCommandService,
    IAlertQueryService alertQueryService
    ) : ControllerBase
{
    /// <summary>
    /// This endpoint retrieves an alert by its unique identifier.
    /// </summary>
    /// <param name="alertId">
    /// The unique identifier of the alert to be retrieved.
    /// </param>
    /// <returns>
    /// The IActionResult containing the alert resource if found, or a NotFound result if not found.
    /// </returns>
    [HttpGet("{alertId}")]
    [SwaggerOperation(
        Summary = "Get Alert by ID",
        Description = "Retrieves an alert by its unique identifier.",
        OperationId = "GetAlertById")]
    [SwaggerResponse(StatusCodes.Status200OK, "Alert found!", typeof(AlertResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Alert not found...")]
    public async Task<IActionResult> GetAlertById([FromRoute] string alertId)
    {
        var alert = await alertQueryService.Handle(new GetAlertByIdQuery(alertId));
        if (alert is null)
        {
            return NotFound($"Alert with ID {alertId} not found...");
        }
        var resource = AlertResourceFromEntityAssembler.ToResourceFromEntity(alert);
        return Ok(resource);
    }
}