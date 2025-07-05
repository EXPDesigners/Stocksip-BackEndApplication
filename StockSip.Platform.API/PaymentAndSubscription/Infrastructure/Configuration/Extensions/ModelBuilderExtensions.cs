using Microsoft.EntityFrameworkCore;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Aggregates;

namespace StockSip.Platform.API.PaymentAndSubscription.Infrastructure.Configuration.Extensions;

/// <summary>
/// This class contains extension methods for the ModelBuilder to apply configurations related to Payment and Subscription domain models.
/// </summary>
public static class ModelBuilderExtensions
{
    public static void ApplyPaymentAndSubscriptionConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Account>().HasKey(a => a.AccountId);
        builder.Entity<Account>().Property(a => a.AccountId).ValueGeneratedOnAdd();

        builder.Entity<Account>().OwnsOne(a => a.BusinessName, bs =>
        {
            bs.WithOwner();
            bs.Property(n => n.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("business_name");
        });

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
                .HasMaxLength(50);
        });

        builder.Entity<Account>().OwnsOne(a => a.StreetAddress, sa =>
        {
            sa.WithOwner();
            sa.Property(st => st.Street)
                .IsRequired()
                .HasMaxLength(200);
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