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
///     Endpoints para gestión de cuentas.
/// </summary>
[Authorize] // → todos requieren token, salvo los marcados con [AllowAnonymous]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Account Endpoints")]
public class AccountsController(
    IAccountCommandService accountCommandService,
    IAccountQueryService   accountQueryService)
    : ControllerBase
{
    #region ──────────── Query Endpoints ────────────

    /// <summary>Obtiene una cuenta por su <paramref name="accountId" />.</summary>
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

    /// <summary>Obtiene una cuenta por su dirección de correo.</summary>
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

    #region ──────────── Command Endpoints ────────────

    /// <summary>Registro (sign‑up) de un usuario y su cuenta asociada.</summary>
    [HttpPost("sign-up")]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary     = "Sign‑up",
        Description = "Creates an authentication user and a linked account.",
        OperationId = "Accounts_SignUp")]
    [ProducesResponseType(typeof(AccountResource), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SignUp([FromBody] SignUpWithAccountResource body)
    {
        var cmd     = SignUpWithAccountFromResourceAssembler.ToCommandFromResource(body);
        var created = await accountCommandService.Handle(cmd);
        if (created is null) return BadRequest("Failed to create account.");

        var resource = AccountResourceFromEntityAssembler.ToResourceFromEntity(created);
        return Ok(resource);
    }

    /// <summary>Crea una cuenta (sin registrar usuario externo).</summary>
    [HttpPost]
    [SwaggerOperation(
        Summary     = "Create Account",
        Description = "Creates a new account with the supplied data.",
        OperationId = "Accounts_Create")]
    [ProducesResponseType(typeof(AccountResource), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAccount([FromBody] CreateAccountResource body)
    {
        var cmd     = CreateAccountCommandFromResourceAssembler.ToCommand(body);
        var created = await accountCommandService.Handle(cmd);
        if (created is null) return BadRequest("Could not create account.");

        var resource = AccountResourceFromEntityAssembler.ToResourceFromEntity(created);
        return CreatedAtAction(
            nameof(GetAccountById),
            new { accountId = resource.AccountId },
            resource);
    }

    #endregion
}
