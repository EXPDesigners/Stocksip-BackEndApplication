namespace StockSip.Platform.API.Authorization.Interfaces.REST.Resources;

public record AuthenticatedUserResource(string userId, string Username, string Token);