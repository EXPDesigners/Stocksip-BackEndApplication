using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using StockSip.Platform.API.AlertsAndNotifications.Domain.Model.Queries;
using StockSip.Platform.API.AlertsAndNotifications.Domain.Model.ValueObjects;
using StockSip.Platform.API.AlertsAndNotifications.Domain.Services;
using StockSip.Platform.API.AlertsAndNotifications.Interfaces.REST.Resources;
using StockSip.Platform.API.AlertsAndNotifications.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace StockSip.Platform.API.AlertsAndNotifications.Interfaces.REST;

[ApiController]
[Route("api/v1/profiles/{profileId}/alerts")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Profiles")]
public class ProfileAlertsController(
    IAlertQueryService alertQueryService
    ) : ControllerBase
{
    [HttpGet]
    [SwaggerOperation(
        Summary = "Get all alerts by profile ID",
        Description = "Retrieves all alerts associated with a specific profile ID.",
        OperationId = "GetAlertsByProfileId")]
    [SwaggerResponse(StatusCodes.Status200OK, "Returns all alerts by profile ID.", typeof(AlertResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "No alerts found for the specified profile.")]
    public async Task<IActionResult> GetAlertsByProfileId(string profileId)
    {
        var targetProfileId = new ProfileId(profileId);
        var getAllAlertsByProfileIdQuery = new GetAllAlertsByProfileIdQuery(targetProfileId);
        var alerts = await alertQueryService.Handle(getAllAlertsByProfileIdQuery);
        var enumerable = alerts.ToList();
        if (enumerable.Count == 0)
        {
            return NotFound(new { Message = "No alerts found for the specified profile." });
        }
        var alertResources = enumerable.Select(AlertResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(alertResources);
    }
}