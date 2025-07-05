using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Entities;
using StockSip.Platform.API.Shared.Domain.Repositories;

namespace StockSip.Platform.API.PaymentAndSubscription.Domain.Repositories;

/// <summary>
/// This contract defines the repository for managing Plan entities.
/// </summary>
public interface IPlanRepository : IBaseRepository<Plan>
{
    
}