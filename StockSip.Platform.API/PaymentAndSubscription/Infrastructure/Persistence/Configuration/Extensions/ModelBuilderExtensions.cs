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

        // BusinessName (VO)
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
                .HasMaxLength(20); // <- se conservan 20 caracteres de la otra versión
        });
        
        builder.Entity<Account>()
            .Property(a => a.CreatedDate)
            .HasConversion(
                v => v.ToDateTime(TimeOnly.MinValue),
                v => DateOnly.FromDateTime(v))
            .IsRequired();
        
        builder.Entity<Account>().OwnsOne(a => a.OwnerUserId, ou =>
        {
            ou.WithOwner();
            ou.Property(o => o.OwnerUserId)
                .IsRequired()
                .HasColumnName("owner_user_id");
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
            money.Property(m => m.Amount).IsRequired();
            money.Property(m => m.Currency).IsRequired().HasMaxLength(3);
        });
        
        
        var freePlan = Plan.CreateFreePlan();
        var monthlyPlan = Plan.CreatePremiumMonthly();
        var annualPlan = Plan.CreatePremiumAnnual();
        
        builder.Entity<Plan>().HasData(
            new 
            {
                freePlan.PlanId,
                freePlan.PlanType,
                freePlan.Description,
                freePlan.PaymentFrequency,
                freePlan.MaxWarehouses,
                freePlan.MaxProducts
            },
            new 
            {
                monthlyPlan.PlanId,
                monthlyPlan.PlanType,
                monthlyPlan.Description,
                monthlyPlan.PaymentFrequency,
                monthlyPlan.MaxWarehouses,
                monthlyPlan.MaxProducts
            },
            new 
            {
                annualPlan.PlanId,
                annualPlan.PlanType,
                annualPlan.Description,
                annualPlan.PaymentFrequency,
                annualPlan.MaxWarehouses,
                annualPlan.MaxProducts
            }
        );

        builder.Entity<Plan>().OwnsOne(p => p.Price).HasData(
            new
            {
                PlanId = freePlan.PlanId,
                Amount = freePlan.Price.Amount,
                Currency = freePlan.Price.Currency
            },
            new
            {
                PlanId = monthlyPlan.PlanId,
                Amount = monthlyPlan.Price.Amount,
                Currency = monthlyPlan.Price.Currency
            },
            new
            {
                PlanId = annualPlan.PlanId,
                Amount = annualPlan.Price.Amount,
                Currency = annualPlan.Price.Currency
            });
    }
}