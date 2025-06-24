using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Aggregates;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Commands;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Repositories;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Services;
using StockSip.Platform.API.Shared.Domain.Repositories;

namespace StockSip.Platform.API.PaymentAndSubscription.Application.Internal.CommandService;

/// <summary>
/// This class implements the IAccountCommandService interface.
/// </summary>
/// <param name="accountRepository"></param>
/// <param name="unitOfWork"></param>
public class AccountCommandService(
    IAccountRepository accountRepository,
    IUnitOfWork unitOfWork) : IAccountCommandService
{
    public async Task<Account?> Handle(CreateAccountCommand command)
    {
        var account = new Account(command);
        await accountRepository.AddAsync(account);
        await unitOfWork.CompleteAsync();
        return account;
    }
}