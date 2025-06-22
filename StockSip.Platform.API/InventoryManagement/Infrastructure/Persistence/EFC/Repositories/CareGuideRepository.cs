using Microsoft.EntityFrameworkCore;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Entities;
using StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;
using StockSip.Platform.API.InventoryManagement.Domain.Repositories;
using StockSip.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using StockSip.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace StockSip.Platform.API.InventoryManagement.Infrastructure.Persistence.EFC.Repositories;

/// <summary>
/// This class implements the ICareGuideRepository interface, providing methods to interact with the CareGuide aggregate.
/// </summary>
public class CareGuideRepository(AppDbContext context) : BaseRepository<CareGuide>(context), ICareGuideRepository
{
    /// <summary>
    /// This async method is used to find a care guide using the associated product id.
    /// </summary>
    /// <param name="productId">
    /// The unique identifier of the associated product.
    /// </param>
    /// <returns>
    /// A Care guide object.
    /// </returns>
    public async Task<CareGuide?> FindByProductIdAsync(string productId)
    {
        return await Context.Set<CareGuide>()
            .FirstOrDefaultAsync(careGuide => careGuide.ProductId == productId);
    }

    /// <summary>
    /// This async method is used to get all the care guides whose owner is a specific account id.
    /// </summary>
    /// <param name="accountId">
    /// The unique identifier of the owner of the care guides.
    /// </param>
    /// <returns>
    /// A list of Care Guide objects.
    /// </returns>
    public async Task<IEnumerable<CareGuide>> FindAllByAccountIdAsync(AccountId accountId)
    {
        return await Context.Set<CareGuide>()
            .Where(careGuide => careGuide.AccountId == accountId)
            .ToListAsync();
    }
}