using Microsoft.EntityFrameworkCore;
using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.Aggregates;
using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.ValueObjects;

namespace StockSip.Platform.API.OrderOperationAndMonitoring.Infrastructure.Persistence.EFC.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyOrderOperationAndMonitoringConfiguration(this ModelBuilder builder)
    {
                builder.Entity<Catalog>(c =>
        {
            c.ToTable("catalogs");

            c.HasKey(x => x.CatalogId);
            c.Property(x => x.CatalogId)
             .HasColumnName("catalog_id")
             .ValueGeneratedOnAdd();

            c.OwnsOne(x => x.AccountId, a =>
            {
                a.Property(p => p.Id)
                 .HasColumnName("account_id")
                 .IsRequired()
                 .HasMaxLength(36);
            });

            c.OwnsOne(x => x.Name, n =>
            {
                n.Property(p => p.Value)
                 .HasColumnName("catalog_name")
                 .IsRequired()
                 .HasMaxLength(100);
            });

            c.OwnsOne(x => x.DateCreated, d =>
            {
                d.Property(p => p.Value)
                 .HasColumnName("date_created")
                 .IsRequired();
            });

            c.Property(x => x.IsPublished)
             .HasColumnName("is_published")
             .IsRequired();

            // relación con Items
            c.HasMany(x => x.Items)
             .WithOne(i => i.Catalog!)
             .HasForeignKey(i => i.CatalogId);
        });

        /* ========== CATALOG ITEM ========== */
        builder.Entity<CatalogItem>(ci =>
        {
            ci.ToTable("catalog_items");

            ci.HasKey(x => x.Id);

            ci.Property(x => x.Id)
              .HasColumnName("id")
              .HasMaxLength(36);

            ci.Property(x => x.CatalogId)
              .HasColumnName("catalog_id")
              .IsRequired();
            
            ci.OwnsOne(x => x.Name, n =>
            {
                n.WithOwner().HasForeignKey("id");
                n.Property(p => p.Name)
                 .HasColumnName("name")
                 .IsRequired()
                 .HasMaxLength(100);
            });

            ci.OwnsOne(x => x.ProductType, pt =>
            {
                pt.WithOwner().HasForeignKey("id");
                pt.Property(p => p.Value)
                  .HasColumnName("product_type")
                  .IsRequired()
                  .HasMaxLength(50);
            });

            ci.OwnsOne(x => x.Brand, b =>
            {
                b.WithOwner().HasForeignKey("id");
                b.Property(p => p.Value)
                 .HasColumnName("brand")
                 .IsRequired()
                 .HasMaxLength(100);
            });

            ci.OwnsOne(x => x.Content, ct =>
            {
                ct.WithOwner().HasForeignKey("id");
                ct.Property(p => p.Value)
                  .HasColumnName("content_ml")
                  .IsRequired();
            });

            ci.Property(x => x.UnitPrice)
              .HasColumnName("unit_price")
              .HasColumnType("decimal(12,2)");

            ci.Property(x => x.DateAdded)
              .HasColumnName("date_added")
              .IsRequired();
        });
        
        builder.Entity<PurchaseOrder>(po =>
{
    po.ToTable("purchase_orders");
    
    po.HasKey(p => p.Id);
    po.Property(p => p.Id)
      .HasColumnName("purchase_order_id")
      .ValueGeneratedOnAdd();
    
    po.OwnsOne(p => p.Date, d =>
    {
        d.Property(x => x.Value)
         .HasColumnName("order_date")
         .IsRequired();
    });
    
    po.Property(p => p.Status)
      .HasColumnName("order_status")
      .HasConversion<string>()
      .IsRequired();
    
    po.OwnsOne(p => p.Buyer, b =>
    {
        b.Property(x => x.AccountId)     .HasColumnName("buyer_account_id");
        b.Property(x => x.UserOwnerId)   .HasColumnName("buyer_user_owner_id");
        b.Property(x => x.Role)          .HasColumnName("buyer_role");
        b.Property(x => x.BusinessName)  .HasColumnName("buyer_business_name");
        b.Property(x => x.Email)         .HasColumnName("buyer_email");
    });
    
    po.OwnsOne(p => p.Supplier, s =>
    {
        s.Property(x => x.AccountId)     .HasColumnName("supplier_account_id");
        s.Property(x => x.UserOwnerId)   .HasColumnName("supplier_user_owner_id");
        s.Property(x => x.Role)          .HasColumnName("supplier_role");
        s.Property(x => x.BusinessName)  .HasColumnName("supplier_business_name");
        s.Property(x => x.Email)         .HasColumnName("supplier_email");
    });
    
    po.OwnsMany(p => p.Items, i =>
    {
        i.ToTable("order_items");
        i.WithOwner().HasForeignKey("purchase_order_id");


        i.Property(p => p.Id)        .HasColumnName("item_id").HasMaxLength(36);
        i.Property(p => p.CatalogId) .HasColumnName("catalog_id");
        i.Property(p => p.Name)      .HasColumnName("name").HasMaxLength(100);
        i.Property(p => p.ProductType).HasColumnName("product_type").HasMaxLength(50);
        i.Property(p => p.Brand)      .HasColumnName("brand").HasMaxLength(100);
        i.Property(p => p.Content)    .HasColumnName("content_ml");
        i.Property(p => p.UnitPrice)  .HasColumnName("unit_price")
                                      .HasColumnType("decimal(12,2)");
        i.Property(p => p.DateAdded)  .HasColumnName("date_added");
        i.Property(p => p.CustomQuantity).HasColumnName("custom_quantity");
    });
    
    po.Property(p => p.TotalAmount)
      .HasColumnName("total_amount")
      .HasColumnType("decimal(14,2)");

    po.Property(p => p.TotalItems)
      .HasColumnName("total_items");
});

    }
}