using StockSip.Platform.API.InventoryManagement.Domain.Model.Commands;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Entities;

namespace StockSip.Platform.API.InventoryManagement.Domain.Services;

public interface ICareGuideCommandService
{
    Task<CareGuide?> Handle(CreateCareGuideCommand command);
    Task<CareGuide?> Handle(CreateCareGuideWithoutProductIdCommand command);
    Task<CareGuide?> Handle(UpdateCareGuideCommand command);
    Task<CareGuide?> Handle(UnassignCareGuideCommand command);
    Task<CareGuide?> Handle(AssignCareGuideToProductCommand command);
}