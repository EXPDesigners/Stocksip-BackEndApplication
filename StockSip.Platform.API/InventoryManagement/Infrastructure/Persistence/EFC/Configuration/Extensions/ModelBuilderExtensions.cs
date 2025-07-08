using Microsoft.EntityFrameworkCore;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Entities;
using StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.InventoryManagement.Infrastructure.Persistence.EFC.Configuration.Extensions;

/// <summary>
/// This static class contains extension methods for the ModelBuilder to apply configuration for the Inventory Management domain model.
/// </summary>
public static class ModelBuilderExtensions
{
    public static void ApplyInventoryManagementConfiguration(this ModelBuilder builder)
    {
        // Inventory Management ORM Mapping Rules
        
        // Warehouse ORM Mapping Rules
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
            i.Property(img => img.ImageUri)
                .IsRequired()
                .HasMaxLength(500)
                .HasColumnName("image_url");
        });
        
        builder.Entity<Warehouse>().Property(w => w.AccountId).HasConversion(v => v.Id, v => new AccountId(v)).IsRequired().HasColumnName("account_id");
        
        // Product ORM Mapping Rules
        builder.Entity<Product>().HasKey(p => p.ProductId);
        builder.Entity<Product>().Property(p => p.ProductId).IsRequired().ValueGeneratedOnAdd();
        
        builder.Entity<Product>().OwnsOne(p => p.ProductName, pn =>
        {
            pn.WithOwner();
            pn.Property(p => p.Name).HasMaxLength(100);
        });
        
        builder.Entity<Product>().OwnsOne(p => p.UnitPrice, up =>
        {
            up.Property(m => m.Amount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            up.Property(m => m.Currency)
                .IsRequired().
                HasMaxLength(3);
        });
        
        builder.Entity<Product>().Property(p => p.Brand).IsRequired().HasMaxLength(50);
        
        builder.Entity<Product>().Property(p => p.LiquorType).HasConversion<string>().HasMaxLength(20).IsRequired();
        
        builder.Entity<Product>().OwnsOne(p => p.MinimumStock, ms =>
        {
            ms.WithOwner();
            ms.Property(msk => msk.MinimumStock).IsRequired();
        });
        
        builder.Entity<Product>().OwnsOne(p => p.ImageUrl, i =>
        {
            i.Property(img => img.ImageUri)
                .IsRequired()
                .HasMaxLength(500)
                .HasColumnName("image_url");
        });

        builder.Entity<Product>().OwnsOne(p => p.AccountId, ac =>
        {
            ac.WithOwner();
            ac.Property(a => a.Id).IsRequired()
                .HasColumnName("account_id");
        });
        
        // Inventory ORM Mapping Rules

        builder.Entity<Inventory>().HasKey(i => i.InventoryId);
        builder.Entity<Inventory>().Property(i => i.InventoryId).IsRequired();

        builder.Entity<Inventory>()
            .Property(i => i.ProductState)
            .HasConversion<string>()
            .IsRequired();

        builder.Entity<Inventory>()
            .HasOne(i => i.Product)
            .WithMany(p => p.Inventories)
            .HasForeignKey(i => i.ProductId);

        builder.Entity<Inventory>()
            .HasOne(i => i.Warehouse)
            .WithMany()
            .HasForeignKey(i => i.WarehouseId);

        builder.Entity<Inventory>().OwnsOne(i => i.ProductStock, ps =>
        {
            ps.WithOwner();
            ps.Property(s => s.Stock).IsRequired();
        });

        builder.Entity<Inventory>().OwnsOne(i => i.ProductBestBeforeDate, b =>
        {
            b.Property(p => p.BestBeforeDate)
                .HasColumnType("date")
                .HasConversion(
                    v => v.ToDateTime(TimeOnly.MinValue),
                    v => DateOnly.FromDateTime(v))
                .IsRequired();
        });
        
        // ProductExit ORM Mapping Rules

        // CareGuide ORM Mapping Rules
        builder.Entity<CareGuide>().HasKey(cg => cg.Id);
        builder.Entity<CareGuide>().Property(cg => cg.Id).IsRequired();
        builder.Entity<CareGuide>().Property(cg => cg.Title).IsRequired().HasMaxLength(200);
        builder.Entity<CareGuide>().Property(cg => cg.Summary).IsRequired().HasMaxLength(1000);
        builder.Entity<CareGuide>().Property(cg => cg.RecommendedMinTemperature).IsRequired();
        builder.Entity<CareGuide>().Property(cg => cg.RecommendedMaxTemperature).IsRequired();
        builder.Entity<CareGuide>().Property(cg => cg.RecommendedPlaceStorage).IsRequired().HasMaxLength(200);
        builder.Entity<CareGuide>().Property(cg => cg.GeneralRecommendation).IsRequired().HasMaxLength(1000);
        builder.Entity<CareGuide>().Property(cg => cg.ProductId).HasMaxLength(100);
        builder.Entity<CareGuide>().Property(cg => cg.AccountId).HasConversion(v => v.Id, v => new AccountId(v)).IsRequired().HasColumnName("account_id");
        builder.Entity<CareGuide>()
            .HasOne(cg => cg.Product)
            .WithMany()
            .HasForeignKey(cg => cg.ProductId)
            .IsRequired(false);
    }
}