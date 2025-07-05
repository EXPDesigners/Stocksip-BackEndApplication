using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Services;
using StockSip.Platform.API.PaymentAndSubscription.Interfaces.REST.Resources;
using StockSip.Platform.API.PaymentAndSubscription.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace StockSip.Platform.API.PaymentAndSubscription.Interfaces.REST;

/// <summary>
/// This controller provides endpoints for managing payments and subscriptions.
/// </summary>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Payment Endpoints")]
public class PaymentsController(ISubscriptionCommandService subscriptionCommandService) : ControllerBase
{
    /// <summary>
    /// This endpoint handles the subscription process for a specified plan.
    /// </summary>
    /// <param name="resource">A resource containing the details of the subscription request.</param>
    /// <returns>An IActionResult containing a redirect URL for payment processing.</returns>
    [HttpPost("subscribe")]
    [SwaggerOperation(
        Summary = "Subscribe to a Plan",
        Description = "Handles the subscription process for a specified plan.",
        OperationId = "SubscribeToPlan")]
    [SwaggerResponse(StatusCodes.Status200OK, "Returns a redirect URL for payment processing.")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid subscription request.")]
    public async Task<IActionResult> Subscribe([FromBody] SubscribeToPlanResource resource)
    {
        var subscribeToPlanCommand = SubscribeToPlanFromResourceAssembler.ToCommandFromResource(resource);
        var redirectUrl = await subscriptionCommandService.Handle(subscribeToPlanCommand);
        return Ok(new { redirectUrl });
    }

    /// <summary>
    /// This endpoint completes the subscription process after payment success.
     /// It is called when the payment gateway redirects back to the application after a successful payment.
    /// </summary>
    /// <param name="resource">A resource containing the details of the subscription completion request.</param>
    /// <returns>An IActionResult indicating the success or failure of the subscription completion.</returns>
    [HttpGet("success")]
    [SwaggerOperation(
        Summary = "Complete Subscription",
        Description = "Completes the subscription process after payment success.",
        OperationId = "CompleteSubscription")]
    [SwaggerResponse(StatusCodes.Status200OK, "Subscription completed successfully.")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid subscription completion request.")]
    public async Task<IActionResult> PaymentSuccess([FromQuery] CompleteSubscriptionResource resource)
    {
        var completeSubscriptionCommand = CompleteSubscriptionFromResourceAssembler.ToCommandFromResource(resource);
        await subscriptionCommandService.Handle(completeSubscriptionCommand);
        return Ok(new { message = "Subscription completed successfully." });
    }
}