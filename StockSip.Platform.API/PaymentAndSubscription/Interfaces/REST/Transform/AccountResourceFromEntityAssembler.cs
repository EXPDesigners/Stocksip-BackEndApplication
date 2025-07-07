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
    /// Converts an <see cref="Account"/> entity to an <see cref="AccountResource"/> DTO.
    /// </summary>
    /// <param name="entity">The <see cref="Account"/> entity to convert.</param>
    /// <returns>The corresponding <see cref="AccountResource"/> DTO.</returns>
    public static AccountResource ToResourceFromEntity(Account entity)
    {
        return new AccountResource(
            entity.AccountId,
            entity.Email.ToString(),
            entity.BusinessName.ToString(),
            entity.Status.ToString(),
            entity.AccountRole.ToString(),
            entity.GetCreationDate()
        );
    }
}