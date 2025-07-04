namespace StockSip.Platform.API.Authorization.Interfaces.REST.Resources;

/// <summary>
/// The resource representing the sign-up request.
/// </summary>
public record SignUpResource(string Username, string Password);