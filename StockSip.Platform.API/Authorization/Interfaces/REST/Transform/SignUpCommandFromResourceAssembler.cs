using StockSip.Platform.API.Authorization.Domain.Model.Commands;
using StockSip.Platform.API.Authorization.Interfaces.REST.Resources;

namespace StockSip.Platform.API.Authorization.Interfaces.REST.Transform;

public static class SignUpCommandFromResourceAssembler
{
    public static SignUpCommand ToCommandFromResource(SignUpResource resource)
    {
        return new SignUpCommand(resource.Username, resource.Password);
    }
}