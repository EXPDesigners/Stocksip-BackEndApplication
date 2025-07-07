using StockSip.Platform.API.Authorization.Domain.Model.Commands;
using StockSip.Platform.API.Authorization.Interfaces.REST.Resources;

namespace StockSip.Platform.API.Authorization.Interfaces.REST.Transform;

/// <summary>
/// This class is responsible for transforming a SendRecoveryCodeResource into a SendRecoveryCodeCommand.
/// </summary>
public class SendRecoveryCodeFromResourceAssembler
{
    /// <summary>
    /// This method transforms a SendRecoveryCodeResource into a SendRecoveryCodeCommand.
    /// </summary>
    public static SendRecoveryCodeCommand ToCommandFromResource(SendRecoveryCodeResource resource)
    {
        return new SendRecoveryCodeCommand(resource.Username);
    }
}