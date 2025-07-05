using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using StockSip.Platform.API.Authorization.Infrastructure.Pipeline.Middleware.Attributes;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Queries;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Services;
using StockSip.Platform.API.PaymentAndSubscription.Interfaces.REST.Resources;
using StockSip.Platform.API.PaymentAndSubscription.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace StockSip.Platform.API.PaymentAndSubscription.Interfaces.REST;

/// <summary>
/// This controller provides endpoints for managing accounts.
/// </summary>
/// <param name="accountCommandService">The command service for handling account operations.</param>
/// <param name="accountQueryService">The query service for retrieving account information.</param>
[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Account Endpoints")]
public class AccountsController(
    IAccountCommandService accountCommandService, 
    IAccountQueryService accountQueryService) : ControllerBase
{

    [HttpPost("sign-up")]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Sing-up",
        Description = "Sign up a new account.",
        OperationId = "CreateAccount")]
    [SwaggerResponse(StatusCodes.Status200OK, "Account created successfully.")]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Failed to create account.")]
    public async Task<IActionResult> CreateAccount([FromBody] SignUpWithAccountResource resource)
    {
        var createAccountCommand = SignUpWithAccountFromResourceAssembler.ToCommandFromResource(resource);
        var account = await accountCommandService.Handle(createAccountCommand);
        if (account is null) return BadRequest("Failed to create account");
        var resourceFromEntity = AccountResourceFromEntityAssembler.ToResourceFromEntity(account);
        return Ok(resourceFromEntity);
    }
 
    /// <summary>
    /// This endpoint retrieves an account by its unique identifier.
    /// </summary>
    /// <param name="accountId">The unique identifier of the account to retrieve.</param>
    /// <returns>An IActionResult containing the account resource if found, or a NotFound result if not found.</returns>
    [SwaggerOperation(
        Summary = "Get Account by ID",
        Description = "Retrieves an account by its unique identifier.",
        OperationId = "GetAccountById")]
    [SwaggerResponse(StatusCodes.Status200OK, "Returns an account by its unique identifier.")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "Account not found")]
    [HttpGet("{accountId}")]
    public async Task<IActionResult> GetAccountById([FromRoute] string accountId)
    {
        var account = await accountQueryService.Handle(new GetAccountByIdQuery(accountId));
        if (account is null) return NotFound($"Account with ID {accountId} not found.");
        var resource = AccountResourceFromEntityAssembler.ToResourceFromEntity(account);
        return Ok(resource);
    }
}