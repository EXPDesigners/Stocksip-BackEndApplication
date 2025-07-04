namespace StockSip.Platform.API.ProfileManagement.Domain.Model;

/// <summary>
/// This query is used to retrieve a profile by its username.
/// </summary>
/// <param name="UserName"></param>
public record GetProfileByUserNameQuery(string UserName);