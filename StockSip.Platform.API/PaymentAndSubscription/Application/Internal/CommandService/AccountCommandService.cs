using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Aggregates;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Commands;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Repositories;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Services;
using StockSip.Platform.API.Shared.Domain.Repositories;

namespace StockSip.Platform.API.PaymentAndSubscription.Application.Internal.CommandService;

public class AccountCommandService(
    IAccountRepository accountRepository,
    IUnitOfWork        unitOfWork) : IAccountCommandService
{
    public async Task<Account?> Handle(CreateAccountCommand command)
    {
        var existing = await accountRepository.FindByEmailAsync(command.Email);
        if (existing is not null)
            throw new ArgumentException($"Email {command.Email} is already in use.");
        
        var account = new Account(command);
        
        await accountRepository.AddAsync(account);
        await unitOfWork.CompleteAsync();

        return account;
    }
}