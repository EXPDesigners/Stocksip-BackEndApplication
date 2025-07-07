using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using StockSip.Platform.API.Authorization.Infrastructure.Pipeline.Middleware.Attributes;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Queries;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Services;
using StockSip.Platform.API.PaymentAndSubscription.Interfaces.Rest.Resources;
using StockSip.Platform.API.PaymentAndSubscription.Interfaces.REST.Resources;
using StockSip.Platform.API.PaymentAndSubscription.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace StockSip.Platform.API.PaymentAndSubscription.Interfaces.REST;

/// <summary>
///     This controller provides endpoints for managing accounts in the Payment and Subscription API.
/// </summary>
[Authorize] 
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Account Endpoints")]
public class AccountsController(
    IAccountCommandService accountCommandService,
    IAccountQueryService   accountQueryService)
    : ControllerBase
{
    
    [HttpPost("sign-up")]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Create Account",
        Description = "Creates a new account with the provided details.",
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
    
    #region 
    
    [HttpGet("{accountId}")]
    [SwaggerOperation(
        Summary     = "Get Account by ID",
        Description = "Retrieves an account by its unique identifier.",
        OperationId = "Accounts_GetById")]
    [ProducesResponseType(typeof(AccountResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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

    #endregion
    
    [HttpGet("{accountId}/status")]
    public async Task<IActionResult> GetAccountStatus([FromRoute] string accountId)
    {
        var account = await accountQueryService.Handle(new GetAccountByIdQuery(accountId));
        if (account is null) return NotFound($"Account with ID {accountId} not found.");
        var resource = new AccountStatusResource(account.Status.ToString());
        return Ok(resource);
    }
}
