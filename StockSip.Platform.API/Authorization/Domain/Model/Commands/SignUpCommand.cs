namespace StockSip.Platform.API.Authorization.Domain.Model.Commands;

/// <summary>
/// The sign-up command.
/// </summary>
public record SignUpCommand(string Username, string Password);