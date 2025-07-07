namespace StockSip.Platform.API.Authorization.Interfaces.REST.Resources;

/// <summary>
/// This record represents a command resource for verifying a recovery code.
/// </summary>
/// <param name="Username">The username of the user who is verifying the recovery code.</param>
/// <param name="RecoveryCode">The recovery code that the user is verifying.</param>
public record VerifyRecoveryCodeResource(string Username, string RecoveryCode);