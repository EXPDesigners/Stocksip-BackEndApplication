using Microsoft.EntityFrameworkCore;
using StockSip.Platform.API.AlertsAndNotifications.Domain.Model.Aggregates;
using StockSip.Platform.API.AlertsAndNotifications.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.AlertsAndNotifications.Infrastructure.Persistence.EFC.Configuration.Extensions;

/// <summary>
/// This static class contains extension methods for the ModelBuilder to apply configuration for the Alerts And Notifications domain model.
/// </summary>
public static class ModelBuilderExtensions
{
    public static void ApplyAlertsAndNotificationsConfiguration(this ModelBuilder builder)
    {
        builder.Entity<Alert>(entity =>
        {
            entity.ToTable("alerts");
            entity.HasKey(a => a.Id);
            entity.Property(a => a.Id).HasColumnName("id");
            entity.Property(a => a.Title).HasColumnName("title");
            entity.Property(a => a.Message).HasColumnName("message");
            entity.Property(a => a.Severity)
                  .HasConversion(
                      v => v.ToString(),
                      v => (ESeverityTypes)Enum.Parse(typeof(ESeverityTypes), v, true))
                  .HasColumnName("severity");
            entity.Property(a => a.Type)
                  .HasConversion(
                      v => v.ToString(),
                      v => (EAlertTypes)Enum.Parse(typeof(EAlertTypes), v, true))
                  .HasColumnName("type");
            entity.Property(a => a.CreatedAt).HasColumnName("created_at");
            entity.Property(a => a.ResolvedAt).HasColumnName("resolved_at");
            entity.Property(a => a.State)
                  .HasConversion(
                      v => v.ToString(),
                      v => (EAlertState)Enum.Parse(typeof(EAlertState), v, true))
                  .HasColumnName("state");
            entity.Property(a => a.AccountId)
                  .HasConversion(
                      v => v.Id,
                      v => new AccountId(v))
                  .HasColumnName("account_id");
            entity.Property(a => a.ProductId)
                  .HasConversion(
                      v => v.Id,
                      v => new ProductId(v))
                  .HasColumnName("product_id");
            entity.Property(a => a.WarehouseId)
                  .HasConversion(
                      v => v.Id,
                      v => new WarehouseId(v))
                  .HasColumnName("warehouse_id");
        });
    }
}