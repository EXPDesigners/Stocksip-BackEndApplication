using StockSip.Platform.API.ProfileManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.ProfileManagement.Domain.Model.ValueObjects;
using StockSip.Platform.API.Shared.Domain.Repositories;

namespace StockSip.Platform.API.ProfileManagement.Domain.Repositories;

/// <summary>
/// This interface defines the contract for a repository that manages profiles.
/// </summary>
public interface IProfileRepository : IBaseRepository<Profile>
{
    /// <summary>
    /// This method retrieves a profile by its unique identifier.
    /// </summary>
    Task<Profile?> FindByUserNameAsync(string userName);
    
    /// <summary>
    /// This method retrieves all the profiles associated with a specific account ID.
    /// </summary>
    Task<IEnumerable<Profile>> FindAllByAccountIdAsync(AccountId accountId);
}