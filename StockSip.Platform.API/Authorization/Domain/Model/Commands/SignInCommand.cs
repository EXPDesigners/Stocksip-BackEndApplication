namespace StockSip.Platform.API.Authorization.Domain.Model.Commands;

/// <summary>
/// The sign-in command.
/// </summary>
public record SignInCommand(string Username, string Password);
