using Microsoft.EntityFrameworkCore;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.InventoryManagement.Infrastructure.Configuration.Extensions;

/// <summary>
/// This static class contains extension methods for the ModelBuilder to apply configuration for the Inventory Management domain model.
/// </summary>
public static class ModelBuilderExtensions
{
    public static void ApplyInventoryManagementConfiguration(this ModelBuilder builder)
    {
        // Inventory Management ORM Mapping Rules
        builder.Entity<Warehouse>().HasKey(w => w.WarehouseId);
        builder.Entity<Warehouse>().Property(w => w.WarehouseId).ValueGeneratedOnAdd();
        builder.Entity<Warehouse>().Property(w => w.Name).IsRequired().HasMaxLength(100);
        
        builder.Entity<Warehouse>().OwnsOne( w => w.Address, a =>
        {
            a.WithOwner();
            a.Property(ad => ad.Street).IsRequired().HasMaxLength(200);
            a.Property(ad => ad.District).IsRequired().HasMaxLength(100);
            a.Property(ad => ad.City).IsRequired().HasMaxLength(100);
            a.Property(ad => ad.Country).IsRequired().HasMaxLength(100);
            a.Property(ad => ad.PostalCode).IsRequired().HasMaxLength(10);
        });
        
        builder.Entity<Warehouse>().OwnsOne(w => w.Temperature, t =>
        {
            t.WithOwner();
            t.Property(temp => temp.MinTemperature).IsRequired();
            t.Property(temp => temp.MaxTemperature).IsRequired();
        });
        
        builder.Entity<Warehouse>().OwnsOne(w => w.Capacity, c =>
        {
            c.WithOwner();
            c.Property(c => c.TotalCapacity).IsRequired();
        });
        
        builder.Entity<Warehouse>().OwnsOne(w => w.ImageUrl, i =>
        {
            i.WithOwner();
            i.Property(img => img.ImageUri).IsRequired().HasMaxLength(500).HasColumnName("image_url");
        });
        
        builder.Entity<Warehouse>().Property(w => w.ProfileId).HasConversion(v => v.Id, v => new ProfileId(v)).IsRequired().HasColumnName("profile_id");
        
    }
    
}