namespace StockSip.Platform.API.Authorization.Interfaces.REST.Resources;

/// <summary>
/// This record represents a resource for sending a recovery code to a user.
/// </summary>
public record SendRecoveryCodeResource(string Username);