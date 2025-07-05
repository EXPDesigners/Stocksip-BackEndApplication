using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using StockSip.Platform.API.Authorization.Domain.Services;
using StockSip.Platform.API.Authorization.Infrastructure.Pipeline.Middleware.Attributes;
using StockSip.Platform.API.Authorization.Interfaces.REST.Resources;
using StockSip.Platform.API.Authorization.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace StockSip.Platform.API.Authorization.Interfaces.REST;

/// <summary>
/// This controller provides endpoints for user authentication, including sign-in and sign-up functionalities.
/// </summary>
/// <param name="userCommandService">The service for handling user commands.</param>
[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Authentication endpoints")]
public class AuthenticationController(IUserCommandService userCommandService) : ControllerBase
{
     /**
     * <summary>
     *     Sign in endpoint. It allows authenticating a user
     * </summary>
     * <param name="signInResource">The sign-in resource containing username and password.</param>
     * <returns>The authenticated user resource, including a JWT token</returns>
     */
    [HttpPost("sign-in")]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Sign in",
        Description = "Sign in a user",
        OperationId = "SignIn")]
    [SwaggerResponse(StatusCodes.Status200OK, "The user was authenticated", typeof(AuthenticatedUserResource))]
    [SwaggerResponse(StatusCodes.Status401Unauthorized, "The sign-in process has failed", typeof(string))]
    public async Task<IActionResult> SignIn([FromBody] SignInResource signInResource)
    {
        try
        {
            var signInCommand = SignInCommandFromResourceAssembler.ToCommandFromResource(signInResource);
            var authenticatedUser = await userCommandService.Handle(signInCommand);
            var resource =
                AuthenticatedUserResourceFromEntityAssembler.ToResourceFromEntity(authenticatedUser.user,
                    authenticatedUser.token, authenticatedUser.accountId);
            return Ok(resource);
        }
        catch (Exception ex)
        {
            return Unauthorized(ex.Message);
        }
    }
}