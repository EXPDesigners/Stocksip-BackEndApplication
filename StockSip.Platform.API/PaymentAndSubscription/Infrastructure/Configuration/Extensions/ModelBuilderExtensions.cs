using Microsoft.EntityFrameworkCore;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Aggregates;

namespace StockSip.Platform.API.PaymentAndSubscription.Infrastructure.Configuration.Extensions;

/// <summary>
/// Extensiones de <see cref="ModelBuilder"/> para el bounded‑context **Payment & Subscription**.
/// Combina reglas de mapeo de ambas ramas: se incluyen <c>Email</c>, <c>StreetAddress</c>
/// y se mantiene <c>AccountRole</c> como *value object* (propiedad <c>Role</c>).
/// </summary>
public static class ModelBuilderExtensions
{
    public static void ApplyPaymentAndSubscriptionConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Account>().HasKey(a => a.AccountId);
        builder.Entity<Account>().Property(a => a.AccountId).ValueGeneratedOnAdd();

        // Business Name (VO)
        builder.Entity<Account>().OwnsOne(a => a.BusinessName, bs =>
        {
            bs.WithOwner();
            bs.Property(n => n.Name)
              .IsRequired()
              .HasMaxLength(50)
              .HasColumnName("business_name");
        });

        // Email (VO)
        builder.Entity<Account>().OwnsOne(a => a.Email, e =>
        {
            e.WithOwner();
            e.Property(v => v.Value)
              .IsRequired()
              .HasMaxLength(100)
              .HasColumnName("email");
        });
        
        builder.Entity<Account>().Property(a => a.Status)
              .HasConversion<string>()
              .HasMaxLength(50)
              .IsRequired();
        
        builder.Entity<Account>().OwnsOne(a => a.AccountRole, r =>
        {
            r.WithOwner();
            r.Property(ar => ar.Role)
              .IsRequired()
              .HasMaxLength(20); // <- se conservan 20 caracteres de la otra versión
        });
        
        builder.Entity<Account>().Property(a => a.CreatedDate).IsRequired();
        
        builder.Entity<Account>().OwnsOne(a => a.OwnerUserId, ou =>
        {
            ou.WithOwner();
            ou.Property(o => o.OwnerUserId)
              .IsRequired()
              .HasColumnName("owner_user_id");
        });
    }
}
