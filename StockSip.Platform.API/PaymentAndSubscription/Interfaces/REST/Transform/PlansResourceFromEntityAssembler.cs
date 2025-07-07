using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Entities;
using StockSip.Platform.API.PaymentAndSubscription.Interfaces.REST.Resources;

namespace StockSip.Platform.API.PaymentAndSubscription.Interfaces.REST.Transform;

public class PlansResourceFromEntityAssembler
{
    public static PlanResource ToResourceFromEntity(Plan entity)
    {
        return new PlanResource(
            entity.PlanId,
            entity.PlanType.ToString(),
            entity.Description,
            entity.Price.Amount,
            entity.MaxWarehouses,
            entity.MaxProducts
        );
    }
    
    public static IEnumerable<PlanResource> ToResourceFromEntities(IEnumerable<Plan> entities)
    {
        return entities.Select(ToResourceFromEntity);
    }
    
}