using StockSip.Platform.API.InventoryManagement.Domain.Model.Entities;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Queries;

namespace StockSip.Platform.API.InventoryManagement.Domain.Services;

public interface ICareGuideQueryService
{
    Task<IEnumerable<CareGuide>> Handle(GetAllCareGuidesByAccountId query);
    
    Task<CareGuide?> Handle(GetCareGuideByIdQuery query);
    Task<CareGuide?> Handle(GetCareGuideByProductIdQuery query);
}