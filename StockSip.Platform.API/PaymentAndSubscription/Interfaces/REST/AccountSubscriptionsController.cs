using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Queries;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Services;
using StockSip.Platform.API.PaymentAndSubscription.Interfaces.REST.Resources;
using Swashbuckle.AspNetCore.Annotations;

namespace StockSip.Platform.API.PaymentAndSubscription.Interfaces.REST;

/// <summary>
/// This controller handles subscription-related operations for accounts.
/// </summary>
/// <param name="subscriptionQueryService">The service to query subscription information.</param>
[ApiController]
[Route("api/v1/accounts/{accountId}/subscriptions")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Accounts")]
public class AccountSubscriptionsController(ISubscriptionQueryService subscriptionQueryService) : ControllerBase
{
    /// <summary>
    /// This method retrieves the current subscription plan for a given account ID.
    /// </summary>
    /// <param name="accountId">The unique identifier of the account for which the current plan is being requested.</param>
    /// <returns>A 200-OK response with the current plan ID if found, or a 404 Not Found response if the account does not exist or has no current plan.</returns>
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
    
    [HttpGet("current-benefits-limits")]
    [SwaggerOperation(
        Summary = "Get Current Benefits Limits with Account ID",
        Description = "Retrieves the current benefits limits for the specified account.",
        OperationId = "GetCurrentBenefitsLimits")]
    [SwaggerResponse(StatusCodes.Status200OK, "Returns the current benefits limits for the account.")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Account not found or no current benefits limits available.")]
    public async Task<IActionResult> GetCurrentBenefitsLimits([FromRoute] string accountId)
    {
        var benefitsLimits = await subscriptionQueryService.Handle(new GetLimitsByAccountId(accountId));
        var resource = new AccountBenefitsLimits(benefitsLimits.Item1, benefitsLimits.Item2);
        return Ok(resource);
    }
}