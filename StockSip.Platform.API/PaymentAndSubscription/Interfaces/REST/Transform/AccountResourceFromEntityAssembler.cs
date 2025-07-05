using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Aggregates;
using StockSip.Platform.API.PaymentAndSubscription.Interfaces.REST.Resources;

namespace StockSip.Platform.API.PaymentAndSubscription.Interfaces.REST.Transform;

/// <summary>
/// Transforma una entidad <see cref="Account"/> en un <see cref="AccountResource"/> DTO.
/// Combina los campos de ambas versiones previas (v1 y v2).
/// </summary>
public static class AccountResourceFromEntityAssembler
{
    /// <summary>
    /// Convierte un <see cref="Account"/> en <see cref="AccountResource"/>.
    /// </summary>
    /// <param name="entity">La entidad de dominio.</param>
    /// <returns>El recurso REST listo para exponer.</returns>
    public static AccountResource ToResourceFromEntity(Account entity)
    {
        return new AccountResource(
            accountId:      entity.AccountId,
            email:          entity.Email.ToString(),          // Nuevo respecto a la primera versión
            businessName:   entity.BusinessName.ToString(),  // Usamos ToString() (≈ Name) para compatibilidad
            status:         entity.Status.ToString(),
            accountRole:    entity.AccountRole.ToString(),
            streetAddress:  entity.StreetAddress.ToString(),  // Nuevo campo
            createdAt:      entity.GetCreationDate()
        );
    }
}