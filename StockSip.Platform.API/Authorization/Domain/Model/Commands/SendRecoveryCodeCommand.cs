namespace StockSip.Platform.API.Authorization.Domain.Model.Commands;

/// <summary>
/// This record represents a command to send a recovery code to a user.
/// </summary>
public record SendRecoveryCodeCommand(string Username);