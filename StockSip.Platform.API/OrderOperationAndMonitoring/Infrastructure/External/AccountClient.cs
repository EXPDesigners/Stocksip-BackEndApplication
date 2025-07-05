using StockSip.Platform.API.InventoryManagement.Domain.External;
using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.External;

namespace StockSip.Platform.API.OrderOperationAndMonitoring.Infrastructure.External;

public sealed class AccountClient(HttpClient http, ILogger<AccountClient> logger)
    : IAccountClient
{
    private const string BasePath = "/api/v1/accounts";

    public async Task<AccountDto?> GetAccountByEmailAsync(string email)
    {
        try
        {
            var uri = $"{BasePath}?email={Uri.EscapeDataString(email)}";
            return await http.GetFromJsonAsync<AccountDto>(uri);
        }
        catch (HttpRequestException ex)
        {
            logger.LogWarning(ex, "Error retrieving account for e‑mail {Email}", email);
            return null;
        }
    }
}