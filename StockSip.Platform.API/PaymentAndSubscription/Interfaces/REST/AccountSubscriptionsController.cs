using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Queries;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace StockSip.Platform.API.PaymentAndSubscription.Interfaces.REST;

[ApiController]
[Route("api/v1/accounts/{accountId}/subscriptions")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Accounts")]
public class AccountSubscriptionsController(ISubscriptionQueryService subscriptionQueryService) : ControllerBase
{
    [HttpGet("current-plan")]
    [SwaggerOperation(
        Summary = "Get Current Subscription Plan with Account ID",
        Description = "Retrieves the current subscription plan for the specified account.",
        OperationId = "GetCurrentPlan")]
    [SwaggerResponse(StatusCodes.Status200OK, "Returns the current plan ID for the account.")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Account not found or no current plan available.")]
    public async Task<IActionResult> GetCurrentPlan([FromRoute] string accountId)
    {
        var planId = await subscriptionQueryService.Handle(new GetPlanIdByAccountIdQuery(accountId));
        return Ok(planId);
        
    }
}