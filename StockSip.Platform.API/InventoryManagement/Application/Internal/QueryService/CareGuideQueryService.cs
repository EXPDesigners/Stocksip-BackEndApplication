using StockSip.Platform.API.InventoryManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Queries;
using StockSip.Platform.API.InventoryManagement.Domain.Repositories;
using StockSip.Platform.API.InventoryManagement.Domain.Services;

namespace StockSip.Platform.API.InventoryManagement.Application.Internal.QueryService;

/// <summary>
/// This class implements the ICareGuideQueryService interface to handle queries related to care guide.
/// </summary>
/// <param name="careGuideRepository">
/// The repository for accessing to care guide data.
/// </param>
public class CareGuideQueryService (
    ICareGuideRepository careGuideRepository
    ) : ICareGuideQueryService
{
    /// <summary>
    /// This async method is used to retrieve all the care guides associated with a specific account id.
    /// </summary>
    /// <param name="query">
    /// The query containing the details to retrieve.
    /// </param>
    /// <returns>
    /// A list of care guide objects.
    /// </returns>
    public async Task<IEnumerable<CareGuide>> Handle(GetAllCareGuidesByAccountId query)
    {
        return await careGuideRepository.FindAllByAccountIdAsync(query.AccountId);
    }

    /// <summary>
    /// This async method is used to retrieve a specific care guide by its id.
    /// </summary>
    /// <param name="query">
    /// The query containing the id of the care guide that will be retrieved.
    /// </param>
    /// <returns>
    /// A care guide object.
    /// </returns>
    public async Task<CareGuide?> Handle(GetCareGuideByIdQuery query)
    {
        return await careGuideRepository.FindByIdAsync(query.Id);
    }

    /// <summary>
    /// This async method is used to retrieve a care guide by the product id.
    /// </summary>
    /// <param name="query">
    /// The query containing the product id.
    /// </param>
    /// <returns>
    /// A care guide object.
    /// </returns>
    public async Task<CareGuide?> Handle(GetCareGuideByProductIdQuery query)
    {
        return await careGuideRepository.FindByProductIdAsync(query.ProductId);
    }
}