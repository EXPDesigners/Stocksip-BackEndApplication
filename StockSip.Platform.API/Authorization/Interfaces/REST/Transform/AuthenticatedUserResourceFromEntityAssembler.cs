using StockSip.Platform.API.Authorization.Domain.Model.Aggregate;
using StockSip.Platform.API.Authorization.Interfaces.REST.Resources;

namespace StockSip.Platform.API.Authorization.Interfaces.REST.Transform;

public static class AuthenticatedUserResourceFromEntityAssembler
{
    public static AuthenticatedUserResource ToResourceFromEntity(
        User   user,
        string token,
        string accountId,
        string accountRole)  
    {
        return new AuthenticatedUserResource(
            user.UserId,
            user.Username,
            token,
            accountId,
            accountRole
        );
    }
}