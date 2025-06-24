using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace StockSip.Platform.API.PaymentAndSubscription.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Account Endpoints")]
public class AccountsController(IAccountCommandService accountCommandService, IAccountQueryService accountQueryService) : ControllerBase
{
    [HttpGet("{accountId}")]
    public async Task<IActionResult> GetAccountById([FromRoute] string accountId)
    {
        //TO-DO: Implement the logic to retrieve the account by ID
        return null;
    }
}