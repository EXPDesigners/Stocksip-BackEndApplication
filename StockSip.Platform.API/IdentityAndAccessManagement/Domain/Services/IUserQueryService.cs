using StockSip.Platform.API.IdentityAndAccessManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.IdentityAndAccessManagement.Domain.Model.Queries;

namespace StockSip.Platform.API.IdentityAndAccessManagement.Domain.Services;

/// <summary>
/// This interface defines the contract for user query services.
/// </summary>
public interface IUserQueryService
{

    /// <summary>
    /// This method retrieves a user by their unique identifier.
    /// </summary>
    Task<User?> Handle(GetUserByIdQuery query);
    
    /// <summary>
    /// This method retrieves a user by their username.
    /// </summary>
    Task<User?> Handle(GetUserByUsernameQuery query);
    
    /// <summary>
    /// This method retrieves all users in the system.
    /// </summary>
    Task<IEnumerable<User>> Handle(GetAllUsersQuery query);

    /// <summary>
    /// This method retrieves all users associated with a specific account by its unique identifier.
    /// </summary>
    Task<IEnumerable<User>> Handle(GetAllUsersByAccountIdQuery query);
}