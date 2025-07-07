using System.Net;
using System.Net.Http.Headers;
using StockSip.Platform.API.InventoryManagement.Domain.External;
using StockSip.Platform.API.OrderOperationAndMonitoring.Domain.External;

namespace StockSip.Platform.API.OrderOperationAndMonitoring.Infrastructure.External;

public sealed class AccountClient : IAccountClient
{
    private const string BasePath = "/api/v1/accounts";

    private readonly HttpClient            _http;
    private readonly IHttpContextAccessor  _ctxAccessor;
    private readonly ILogger<AccountClient> _logger;

    public AccountClient(
        HttpClient http,
        IHttpContextAccessor ctxAccessor,
        ILogger<AccountClient> logger)
    {
        _http         = http;
        _ctxAccessor  = ctxAccessor;
        _logger       = logger;
    }

    public async Task<AccountDto?> GetAccountByEmailAsync(string email)
    {
        var uri = $"{BasePath}?email={Uri.EscapeDataString(email)}";

        // ─── 1) Propagar token ─────────────────────────────────────────────────────
        var bearer = _ctxAccessor.HttpContext?
                               .Request
                               .Headers["Authorization"]
                               .FirstOrDefault();           // "Bearer eyJhbG..."

        if (!string.IsNullOrWhiteSpace(bearer))
        {
            // reemplazamos cualquier valor anterior para no acumular cabeceras
            _http.DefaultRequestHeaders.Authorization =
                AuthenticationHeaderValue.Parse(bearer);
        }
        else
        {
            _logger.LogWarning("AccountClient → llamada sin token a {Uri}", uri);
        }

        // ─── 2) LLamada y manejo de errores ───────────────────────────────────────
        try
        {
            return await _http.GetFromJsonAsync<AccountDto>(uri);
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.NotFound)
        {
            _logger.LogWarning("AccountClient → 404 No account for {Email}", email);
            return null;
        }
        catch (HttpRequestException ex) when (ex.StatusCode == HttpStatusCode.Unauthorized)
        {
            _logger.LogWarning("AccountClient → 401 Unauthorized al consultar {Email}", email);
            return null;
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "AccountClient → error al consultar cuenta {Email}", email);
            return null;
        }
    }
}