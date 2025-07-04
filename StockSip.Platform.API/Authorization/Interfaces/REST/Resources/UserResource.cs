namespace StockSip.Platform.API.Authorization.Interfaces.REST.Resources;

/// <summary>
/// The resource representing a user.
/// </summary>
public record UserResource(string UserId, string Username);