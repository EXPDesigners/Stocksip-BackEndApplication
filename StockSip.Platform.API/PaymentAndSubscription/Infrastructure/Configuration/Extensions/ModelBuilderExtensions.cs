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
        // Payment and Subscription Domain Configuration
        
        // Account ORM Mapping Rules
        builder.Entity<Account>().HasKey(a => a.AccountId);
        builder.Entity<Account>().Property(a => a.AccountId).ValueGeneratedOnAdd();

        builder.Entity<Account>().OwnsOne(a => a.BusinessName, bs =>
        {
            bs.WithOwner();
            bs.Property(nm => nm.Name).IsRequired().HasMaxLength(50).HasColumnName("business_name");
        });
        
        builder.Entity<Account>().Property(a => a.Status).HasConversion<string>().HasMaxLength(50).IsRequired();

        builder.Entity<Account>().Property(p => p.AccountRole).HasConversion<string>().HasMaxLength(20).IsRequired();

        builder.Entity<Account>().Property(a => a.CreatedDate).IsRequired();

        builder.Entity<Account>().OwnsOne(a => a.OwnerUserId, ou =>
        {
            ou.WithOwner();
            ou.Property(o => o.OwnerUserId).IsRequired();
        });

        // Subscription ORM Mapping Rules

        // Subscription Plan ORM Mapping Rules
    }
}