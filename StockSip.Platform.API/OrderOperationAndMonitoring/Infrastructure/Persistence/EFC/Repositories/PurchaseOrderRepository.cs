using Microsoft.EntityFrameworkCore;
using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.Aggregates;
using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Repositories;
using StockSip.Platform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using StockSip.Platform.API.Shared.Infrastructure.Persistence.EFC.Repositories;

namespace StockSip.Platform.API.OrderOperationAndMonitoring.Infrastructure.Persistence.EFC.Repositories;

public class PurchaseOrderRepository(AppDbContext context)
    : BaseRepository<PurchaseOrder>(context), IPurchaseOrderRepository
{
    private DbSet<PurchaseOrder> Orders => Context.Set<PurchaseOrder>();

    public async Task<IEnumerable<PurchaseOrder>> FindByOrderIdAsync(long orderId) =>
        await Orders.Where(o => o.Id == orderId).ToListAsync();

    public async Task<IEnumerable<PurchaseOrder>> FindByBuyerAccountIdAsync(string accountId) =>
        await Orders.Where(o => o.Buyer.AccountId == accountId).ToListAsync();

    public async Task<IEnumerable<PurchaseOrder>> FindBySupplierAccountIdAsync(string accountId) =>
        await Orders.Where(o => o.Supplier.AccountId == accountId).ToListAsync();
}