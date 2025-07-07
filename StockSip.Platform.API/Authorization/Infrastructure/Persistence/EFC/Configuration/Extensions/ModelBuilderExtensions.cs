using Microsoft.EntityFrameworkCore;
using StockSip.Platform.API.Authorization.Domain.Model.Aggregate;

namespace StockSip.Platform.API.Authorization.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyAuthenticationConfiguration(this ModelBuilder builder)
    {
        // Authentication Context
        
        // User Entity Configuration
        builder.Entity<User>().HasKey(u => u.UserId);
        builder.Entity<User>().Property(u => u.UserId).IsRequired();
        builder.Entity<User>().Property(u => u.Username).IsRequired();
        builder.Entity<User>().Property(u => u.PasswordHash).IsRequired();
        builder.Entity<User>().Property(u => u.RecoveryCode).HasMaxLength(10).IsRequired(false);
        builder.Entity<User>().Property(u => u.RecoveryCodeExpiration).IsRequired(false);
    }
}