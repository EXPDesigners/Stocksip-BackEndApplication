using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Aggregates;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Commands;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Services;

namespace StockSip.Platform.API.PaymentAndSubscription.Application.Internal.CommandService;

public class AccountCommandService : IAccountCommandService
{
    public Task<Account?> Handle(CreateAccountCommand command)
    {
        throw new NotImplementedException();
    }
}