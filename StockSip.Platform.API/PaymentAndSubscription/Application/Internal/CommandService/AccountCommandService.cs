using StockSip.Platform.API.PaymentAndSubscription.Application.Internal.OutboundServices.ACL;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Aggregates;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Commands;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Repositories;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Services;
using StockSip.Platform.API.PaymentAndSubscription.Interfaces.ACL;
using StockSip.Platform.API.Shared.Domain.Repositories;

namespace StockSip.Platform.API.PaymentAndSubscription.Application.Internal.CommandService;

public class AccountCommandService(IAccountRepository accountRepository, 
                                    IExternalAuthenticationService externalAuthenticationService, IUnitOfWork unitOfWork) : IAccountCommandService
{
    public async Task<Account?> Handle(SignUpWithAccountCommand command)
    {
        if (command.Password != command.ValidatePassword)
            throw new ArgumentException("Passwords do not match.");
        
        var userId = await externalAuthenticationService.CreateUserAsync(command.Username, command.Password);
        
        var account = new Account(userId, command.AccountRole, command.BusinessName);
        await accountRepository.AddAsync(account);
        await unitOfWork.CompleteAsync();
        return account;
    }
}