namespace StockSip.Platform.API.Authorization.Domain.Model.Commands;

/// <summary>
/// This record represents a command to reset a user's password.
/// </summary>
public record ResetPasswordCommand(string Username, string NewPassword);