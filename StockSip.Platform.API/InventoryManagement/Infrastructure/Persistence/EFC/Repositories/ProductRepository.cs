using Microsoft.EntityFrameworkCore;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Aggregates;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Entities;
using StockSip.Platform.API.InventoryManagement.Domain.Model.Repositories;
using StockSip.Platform.API.InventoryManagement.Domain.Model.ValueObjects;
using StockSip.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using StockSip.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace StockSip.Platform.API.InventoryManagement.Infrastructure.Persistence.EFC.Repositories;

public class ProductRepository(AppDbContext context) : BaseRepository<Product>(context), IProductRepository
{
    public async Task<IEnumerable<Product>> FindByProviderIdAsync(ProviderId providerId)
    {
        return await Context.Set<Product>()
            .Where(product => product.ProviderId == providerId)
            .ToListAsync();
    }

    public async Task<IEnumerable<Inventory>> FindByWarehouseIdAsync(string warehouseId)
    {
        return await Context.Set<Inventory>()
            .Where(inventory => inventory.WarehouseId == warehouseId)
            .Include(inventory => inventory.Product)
            .ToListAsync();
    }

    public async Task<bool> ExistsByIdAsync(string productId)
    {
        return await Context.Set<Product>().AnyAsync(product => product.Id == productId);
    }
}