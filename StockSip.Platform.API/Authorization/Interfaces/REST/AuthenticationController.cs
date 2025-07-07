using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using StockSip.Platform.API.Authorization.Domain.Services;
using StockSip.Platform.API.Authorization.Infrastructure.Pipeline.Middleware.Attributes;
using StockSip.Platform.API.Authorization.Interfaces.REST.Resources;
using StockSip.Platform.API.Authorization.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace StockSip.Platform.API.Authorization.Interfaces.REST;

/// <summary>
///     Endpoints de autenticación (sign‑in / sign‑up).
/// </summary>
[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Authentication endpoints")]
public class AuthenticationController(IUserCommandService userCommandService) : ControllerBase
{
    /// <summary>
    /// Authenticates a user and retrieves a JWT upon successful validation of credentials.
    /// </summary>
    /// <param name="signInResource">The resource containing the user's sign-in credentials (username and password).</param>
    /// <returns>
    /// An <see cref="IActionResult"/> containing either:
    /// - A 200 OK response with the authenticated user details and JWT if authentication is successful.
    /// - A 401 Unauthorized response with an error message if authentication fails.
    /// </returns>
    [HttpPost("sign-in")]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary     = "Sign in",
        Description = "Authenticate a user and obtain a JWT.",
        OperationId = "SignIn")]
    [SwaggerResponse(StatusCodes.Status200OK, "User authenticated", typeof(AuthenticatedUserResource))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "Invalid credentials", typeof(string))]
    public async Task<IActionResult> SignIn([FromBody] SignInResource signInResource)
    {
        try
        {
            var cmd  = SignInCommandFromResourceAssembler.ToCommandFromResource(signInResource);
            var auth = await userCommandService.Handle(cmd);

            var resource = AuthenticatedUserResourceFromEntityAssembler.ToResourceFromEntity(
                auth.user,
                auth.token,
                auth.accountId,
                auth.accountRole
            );

            return Ok(resource);
        }
        catch (Exception ex)
        {
            return Unauthorized(ex.Message);
        }
    }
}
