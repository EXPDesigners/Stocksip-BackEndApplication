using EntityFrameworkCore.CreatedUpdatedDate.Extensions;
using Microsoft.EntityFrameworkCore;
using StockSip.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;

namespace StockSip.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;

/// <summary>
///     Application database context
/// </summary>
public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder builder)
    {
        // Add the created and updated interceptor
        builder.AddCreatedUpdatedInterceptor();
        base.OnConfiguring(builder);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        // Use snake case naming convention for the database
        builder.UseSnakeCaseNamingConvention();
    }
}