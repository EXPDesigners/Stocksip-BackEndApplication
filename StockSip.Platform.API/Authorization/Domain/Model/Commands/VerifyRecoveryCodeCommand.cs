namespace StockSip.Platform.API.Authorization.Domain.Model.Commands;

/// <summary>
/// This command is used to verify a recovery code for a user.
/// </summary>
/// <param name="code">The recovery code to verify.</param>
public record VerifyRecoveryCodeCommand(string username, string RecoverCode);