using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Entities;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Repositories;
using StockSip.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using StockSip.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace StockSip.Platform.API.PaymentAndSubscription.Infrastructure.Persistence.Repositories;

/// <summary>
/// Defines the repository for managing Plan entities.
/// </summary>
public class PlanRepository(AppDbContext context) : BaseRepository<Plan>(context), IPlanRepository
{
    
}