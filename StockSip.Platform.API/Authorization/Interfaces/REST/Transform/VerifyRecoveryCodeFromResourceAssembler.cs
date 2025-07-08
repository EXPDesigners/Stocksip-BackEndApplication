using StockSip.Platform.API.Authorization.Domain.Model.Commands;
using StockSip.Platform.API.Authorization.Interfaces.REST.Resources;

namespace StockSip.Platform.API.Authorization.Interfaces.REST.Transform;

public class VerifyRecoveryCodeFromResourceAssembler
{
    public static VerifyRecoveryCodeCommand ToCommandFromResource(VerifyRecoveryCodeResource resource)
    {
        return new VerifyRecoveryCodeCommand(resource.Username, resource.RecoveryCode);
    }
}