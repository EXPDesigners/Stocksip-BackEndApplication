using StockSip.Platform.API.InventoryManagement.Domain.External;

namespace StockSip.Platform.API.OrderOperationAndMonitoring.Domain.External;

public interface IAccountClient
{
    Task<AccountDto?> GetAccountByEmailAsync(string email);
}