using System.Security.Authentication;
using StockSip.Platform.API.Authorization.Application.Internal.OutboundServices;
using StockSip.Platform.API.Authorization.Domain.Model.Queries;
using StockSip.Platform.API.Authorization.Domain.Services;
using StockSip.Platform.API.Authorization.Infrastructure.Pipeline.Middleware.Attributes;

namespace StockSip.Platform.API.Authorization.Infrastructure.Pipeline.Middleware.Components;

/// <summary>
/// RequestAuthorizationMiddleware is custom middleware.
/// This middleware is used to authorize requests.
/// It validates a token is included in the request header and that the token is valid.
/// If the token is valid, then it sets the user in HttpContext.Items["User"].
/// </summary>
public class RequestAuthorizationMiddleware(RequestDelegate next,
                                           ILogger<RequestAuthorizationMiddleware> logger)
{
    private readonly ILogger<RequestAuthorizationMiddleware> _logger = logger;

    public async Task InvokeAsync(
        HttpContext     context,
        IUserQueryService userQueryService,
        ITokenService     tokenService)
    {
        _logger.LogInformation("Entering InvokeAsync");
            
        if (context.Request.Method.Equals("OPTIONS", StringComparison.OrdinalIgnoreCase))
        {
            await next(context);
            return;
        }
        
        var endpoint  = context.GetEndpoint();
        var allowAnon = endpoint?.Metadata.Any(m => m is AllowAnonymousAttribute) ?? false;

        _logger.LogInformation("Allow Anonymous = {AllowAnonymous}", allowAnon);
        if (allowAnon)
        {
            await next(context);
            return;
        }
        
        var token = context.Request.Headers["Authorization"]
                               .FirstOrDefault()?
                               .Split(' ')
                               .Last();

        if (string.IsNullOrWhiteSpace(token))
        {
            _logger.LogWarning("Missing token");
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Missing or invalid token");
            return;
        }
        
        var userId = await tokenService.ValidateToken(token);
        if (userId is null)
        {
            _logger.LogWarning("Invalid token");
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsync("Invalid token");
            return;
        }
        
        var user = await userQueryService.Handle(new GetUserByIdQuery(userId));
        _logger.LogInformation("Successful authorization. Setting HttpContext.Items[\"User\"]");
        context.Items["User"] = user;

        await next(context);
    }
}