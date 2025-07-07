using StockSip.Platform.API.Authorization.Domain.Model.Aggregate;
using StockSip.Platform.API.Authorization.Domain.Model.Queries;

namespace StockSip.Platform.API.Authorization.Domain.Services;

/// <summary>
/// Defines the contract for user query services.
/// </summary>
public interface IUserQueryService
{
    /// <summary>
    /// Handle the query to get a user by their ID.
    /// </summary>
    Task<User?> Handle(GetUserByIdQuery query);
    
    /// <summary>
    /// Handle the query to get a user by their username.
    /// </summary>
    Task<User?> Handle(GetUserByUsernameQuery query);
}