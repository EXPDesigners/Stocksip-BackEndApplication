using StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.Entities;

public class Catalog(
    int id,
    string name,
    bool isPublished,
    ProfileId profileId
)
{
    public int Id { get; } = id;
    public string Name { get; } = name;
    public bool IsPublished { get; } = isPublished;
    public ProfileId ProfileId { get; } = profileId;
}