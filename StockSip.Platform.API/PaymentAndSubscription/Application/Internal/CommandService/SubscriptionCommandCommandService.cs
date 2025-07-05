using StockSip.Platform.API.PaymentAndSubscription.Application.Internal.OutboundServices.PayPal;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Aggregates;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Commands;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.ValueObjects;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Repositories;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Services;
using StockSip.Platform.API.Shared.Domain.Repositories;

namespace StockSip.Platform.API.PaymentAndSubscription.Application.Internal.CommandService;

public class SubscriptionCommandCommandService(IAccountRepository accountRepository,
                                               IPlanRepository planRepository,
                                               ISubscriptionRepository subscriptionRepository,
                                               IPaymentService paymentService,
                                               IUnitOfWork unitOfWork) : ISubscriptionCommandService
{
    
    public async Task<string?> Handle(SubscribeToPlanCommand command) {
        
        var account = await accountRepository.FindByIdAsync(command.AccountId)
                      ?? throw new Exception("Not Found Account");

        var plan = await planRepository.FindByIdAsync(command.PlanId)
                   ?? throw new Exception("Not Found Plan");

        var approvalUrl = await paymentService.CreateOrder(
            plan.PlanType.ToString(),
            plan.Price.Amount,
            returnUrl: $"http://localhost:5173/payments-success?accountId={command.AccountId}&planId={command.PlanId}",
            cancelUrl: "http://localhost:5173/payments-cancel"
        );

        return approvalUrl;
    }

    public async Task Handle(CompleteSubscriptionCommand command)
    {
        var account = await accountRepository.FindByIdAsync(command.AccountId)
                      ?? throw new Exception("Account not found");

        var plan = await planRepository.FindByIdAsync(command.PlanId)
                   ?? throw new Exception("Plan not found");

        await paymentService.CaptureOrder(command.Token);

        var expiredDate = plan.PaymentFrequency switch
        {
            EPaymentFrequency.Monthly => DateTime.UtcNow.AddMonths(1),
            EPaymentFrequency.Annual => DateTime.UtcNow.AddYears(1),
            _ => throw new Exception("Invalid payment frequency")
        };

        var subscription = new Subscription(account.AccountId, plan, expiredDate);
        subscription.MarkAsCompleted();

        account.ActiveAccount();

        await subscriptionRepository.AddAsync(subscription);
        accountRepository.Update(account);
        await unitOfWork.CompleteAsync();
    }
}