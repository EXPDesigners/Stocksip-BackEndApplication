using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Queries;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Services;
using StockSip.Platform.API.PaymentAndSubscription.Interfaces.Rest.Resources;
using StockSip.Platform.API.PaymentAndSubscription.Interfaces.REST.Resources;
using StockSip.Platform.API.PaymentAndSubscription.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace StockSip.Platform.API.PaymentAndSubscription.Interfaces.REST;

/// <summary>
/// This controller provides endpoints for managing accounts.
/// </summary>
/// <param name="accountCommandService">The command service for handling account operations.</param>
/// <param name="accountQueryService">The query service for retrieving account information.</param>
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Account Endpoints")]
public class AccountsController(
    IAccountCommandService accountCommandService, 
    IAccountQueryService accountQueryService) : ControllerBase
{
 
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
    
    [HttpGet]
    [SwaggerOperation(
        Summary     = "Get Account by Email",
        Description = "Returns the account that matches the given e‑mail address.",
        OperationId = "Accounts_GetByEmail")]
    [ProducesResponseType(typeof(AccountResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAccountByEmail([FromQuery] string email)
    {
        var account = await accountQueryService.Handle(new GetAccountByEmailQuery(email));
        if (account is null) return NotFound($"Account with e‑mail {email} not found.");

        var resource = AccountResourceFromEntityAssembler.ToResourceFromEntity(account);
        return Ok(resource);
    }
    
    [HttpPost]
    [SwaggerOperation(
        Summary     = "Create Account",
        Description = "Creates a new account with the supplied data.",
        OperationId = "Accounts_Create")]
    [ProducesResponseType(typeof(AccountResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAccount([FromBody] CreateAccountResource body)
    {
        var cmd      = CreateAccountCommandFromResourceAssembler.ToCommand(body);
        var created  = await accountCommandService.Handle(cmd);
        if (created is null) return BadRequest("Could not create account.");

        var resource = AccountResourceFromEntityAssembler.ToResourceFromEntity(created);
        return CreatedAtAction(
            nameof(GetAccountById),
            new { accountId = resource.AccountId },
            resource);
    }
}