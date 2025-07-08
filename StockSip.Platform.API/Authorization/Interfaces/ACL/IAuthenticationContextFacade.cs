namespace StockSip.Platform.API.Authorization.Interfaces.ACL;

public interface IAuthenticationContextFacade
{
    Task<string> CreateUserAsync(string username, string password);
}