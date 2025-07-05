using Microsoft.EntityFrameworkCore;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Aggregates;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Entities;

namespace StockSip.Platform.API.PaymentAndSubscription.Infrastructure.Persistence.Configuration.Extensions;

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
        
        builder.Entity<Subscription>().HasKey(s => s.SubscriptionId);
        builder.Entity<Subscription>().Property(s => s.SubscriptionId).ValueGeneratedOnAdd();
        
        builder.Entity<Subscription>().HasOne(s => s.Account)
            .WithMany()
            .HasForeignKey(s => s.AccountId)
            .IsRequired();
        
        builder.Entity<Subscription>().HasOne(s => s.Plan)
            .WithMany()
            .HasForeignKey(s => s.PlanId)
            .IsRequired();

        builder.Entity<Subscription>().Property(s => s.SubscriptionStatus)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Entity<Subscription>().Property(s => s.CreatedDate).IsRequired();
        builder.Entity<Subscription>().Property(s => s.ExpiredDate).IsRequired();

        // Plan ORM Mapping Rules
        builder.Entity<Plan>().HasKey(p => p.PlanId);
        builder.Entity<Plan>().Property(p => p.PlanId).ValueGeneratedOnAdd();

        builder.Entity<Plan>().Property(p => p.Description).IsRequired().HasMaxLength(100);

        builder.Entity<Plan>().Property(p => p.PlanType)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Entity<Plan>().Property(p => p.PaymentFrequency)
            .HasConversion<string>()
            .HasMaxLength(20)
            .IsRequired();

        builder.Entity<Plan>().Property(p => p.MaxWarehouses).IsRequired();
        builder.Entity<Plan>().Property(p => p.MaxProducts).IsRequired();

        builder.Entity<Plan>().OwnsOne(p => p.Price, money =>
        {
            money.WithOwner();
            money.Property(m => m.Amount).HasColumnName("price").IsRequired();
        });
    }
}