namespace StockSip.Platform.API.Authorization.Interfaces.REST.Resources;

/// <summary>
/// The resource representing the sign-in request.
/// </summary>
public record SignInResource(string Username, string Password);