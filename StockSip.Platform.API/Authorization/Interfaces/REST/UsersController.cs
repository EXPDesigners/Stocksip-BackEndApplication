using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using StockSip.Platform.API.Authorization.Domain.Model.Queries;
using StockSip.Platform.API.Authorization.Domain.Services;
using StockSip.Platform.API.Authorization.Infrastructure.Pipeline.Middleware.Attributes;
using StockSip.Platform.API.Authorization.Interfaces.REST.Resources;
using StockSip.Platform.API.Authorization.Interfaces.REST.Transform;
using Swashbuckle.AspNetCore.Annotations;

namespace StockSip.Platform.API.Authorization.Interfaces.REST;

/**
 * <summary>
 *     The user's controller
 * </summary>
 * <remarks>
 *     This class is used to handle user requests
 * </remarks>
 */
[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available User endpoints")]
public class UsersController(IUserQueryService userQueryService, IUserCommandService userCommandService) : ControllerBase
{
    /// <summary>
    /// This endpoint retrieves a user by its ID.
    /// </summary>
    /// <param name="id">The ID of the user to retrieve.</param>
    /// <returns>A 200 OK response with the user resource if found, or a 404 Not Found if the user does not exist.</returns>
    [HttpGet("{id}")]
    [SwaggerOperation(
        Summary = "Get a user by its id",
        Description = "Get a user by its id",
        OperationId = "GetUserById")]
    [SwaggerResponse(StatusCodes.Status200OK, "The user was found", typeof(UserResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The user was not found", typeof(string))]
    public async Task<IActionResult> GetUserById(string id)
    {
        var getUserByIdQuery = new GetUserByIdQuery(id);
        var user = await userQueryService.Handle(getUserByIdQuery);
        var userResource = UserResourceFromEntityAssembler.ToResourceFromEntity(user!);
        return Ok(userResource);
    }

    [HttpPost("send-recovery-code")]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Send a recovery code to the user",
        Description = "Send a recovery code to the user",
        OperationId = "SendRecoveryCode")]
    [SwaggerResponse(StatusCodes.Status200OK, "Recovery code sent successfully", typeof(object))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request", typeof(string))]
    public async Task<IActionResult> SendRecoveryCode([FromBody] SendRecoveryCodeResource resource)
    {
        var command = SendRecoveryCodeFromResourceAssembler.ToCommandFromResource(resource);
        await userCommandService.Handle(command);
        return Ok(new { Message = "Recovery code sent successfully." });
    }
    
    [HttpPost("verify-recovery-code")]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Verify a recovery code",
        Description = "Verify a recovery code",
        OperationId = "VerifyRecoveryCode")]
    [SwaggerResponse(StatusCodes.Status200OK, "Recovery code verified successfully", typeof(object))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid recovery code", typeof(string))]
    public async Task<IActionResult> VerifyRecoveryCode([FromBody] VerifyRecoveryCodeResource resource)
    {
        var command = VerifyRecoveryCodeFromResourceAssembler.ToCommandFromResource(resource);
        await userCommandService.Handle(command);
        return Ok(new { Message = "Recovery code verified successfully." });
    }
    
    [HttpPost("reset-password")]
    [AllowAnonymous]
    [SwaggerOperation(
        Summary = "Reset user password",
        Description = "Reset user password using a recovery code",
        OperationId = "ResetPassword")]
    [SwaggerResponse(StatusCodes.Status200OK, "Password reset successfully", typeof(object))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "Invalid request", typeof(string))]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordResource resource)
    {
        var command = ResetPasswordFromResourceAssembler.ToCommandFromResource(resource);
        await userCommandService.Handle(command);
        return Ok(new { Message = "Password reset successfully." });
    }
}