namespace StockSip.Platform.API.Authorization.Interfaces.REST.Resources;

public record AuthenticatedUserResource(
    string UserId,
    string Username,
    string Token,
    string AccountId,
    string AccountRole 
);