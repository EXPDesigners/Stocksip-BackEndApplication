using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using StockSip.Platform.API.Authorization.Infrastructure.Persistence.EFC.Configuration.Extensions;          // Auth
using StockSip.Platform.API.InventoryManagement.Infrastructure.Persistence.EFC.Configuration.Extensions;   // Inventory
using StockSip.Platform.API.OrderOperationAndMonitoring.Infrastructure.Persistence.EFC.Configuration.Extensions; // Orders & Monitoring
using StockSip.Platform.API.PaymentAndSubscription.Infrastructure.Persistence.Configuration.Extensions;
using StockSip.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;               // Shared (snake‑case, etc.)

namespace StockSip.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;

/// <summary>
///     Application database context — agrega todas las configuraciones de los bounded contexts.
/// </summary>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        // Auditing interceptor (CreatedAt / UpdatedAt)
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // ─── Bounded‑context configurations ───────────────────────────────────────
        builder.ApplyInventoryManagementConfiguration();
        builder.ApplyPaymentAndSubscriptionConfiguration();
        builder.ApplyOrderOperationAndMonitoringConfiguration();
        builder.ApplyAuthenticationConfiguration();

        // ─── Conventions ──────────────────────────────────────────────────────────
        builder.UseSnakeCaseNamingConvention();
    }
}