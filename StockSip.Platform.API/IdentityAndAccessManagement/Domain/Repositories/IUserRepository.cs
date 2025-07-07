using StockSip.Platform.API.IdentityAndAccessManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.IdentityAndAccessManagement.Domain.Model.ValueObjects;
using StockSip.Platform.API.Shared.Domain.Repositories;

namespace StockSip.Platform.API.IdentityAndAccessManagement.Domain.Repositories;

/// <summary>
/// This interface defines the contract for user repository operations.
/// </summary>
public interface IUserRepository : IBaseRepository<User>
{
    
    /// <summary>
    /// This method is used to find all users associated with a specific account ID.
    /// </summary>
    Task<IEnumerable<User>> FindAllByAccountIdAsync(AccountId accountId);
    
    /// <summary>
    /// This method is used to find a user by their unique username.
    /// </summary>
    Task<User> FindByUsernameAsync(string username);
    
    /// <summary>
    /// This method checks if a user exists by their unique username.
    /// </summary>
    bool ExistsByUsername(string username);
}