using StockSip.Platform.API.Authorization.Domain.Model.Aggregate;
using StockSip.Platform.API.Shared.Domain.Repositories;

namespace StockSip.Platform.API.Authorization.Domain.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    /// <summary>
    /// Find a user by their username.
    /// </summary>
    Task<User> FindByUsernameAsync(string username);
    
    /// <summary>
    /// Exists a user by their username.
    /// </summary>
    bool ExistsByUsername(string username);
}