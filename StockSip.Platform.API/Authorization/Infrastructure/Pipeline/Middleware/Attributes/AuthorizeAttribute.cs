using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StockSip.Platform.API.Authorization.Domain.Model.Aggregate;

namespace StockSip.Platform.API.Authorization.Infrastructure.Pipeline.Middleware.Attributes;

/// <summary>
/// This attribute is used to authorize access to a controller or action method.
/// </summary>
public class AuthorizeAttribute : Attribute, IAuthorizationFilter
{
    /// <summary>
    /// This method is called to authorize access to a controller or action method.
    /// It checks if the user is signed in by verifying if the HttpContext.User is set.
    /// If a user is not signed in, it returns a 401 Unauthorized status code.
    /// </summary>
    /// <param name="context"></param>
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();

        if (allowAnonymous)
        {
            Console.WriteLine(" Skipping authorization");
            return;
        }

        // verify if a user is signed in by checking if HttpContext.User is set
        var user = (User?)context.HttpContext.Items["User"];

        // if a user is not signed in, then return 401-status code
        if (user == null) context.Result = new UnauthorizedResult();
    }
}