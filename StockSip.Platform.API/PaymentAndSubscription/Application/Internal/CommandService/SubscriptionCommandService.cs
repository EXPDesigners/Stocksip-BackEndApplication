using StockSip.Platform.API.PaymentAndSubscription.Application.Internal.OutboundServices.PayPal;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Aggregates;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Commands;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Model.ValueObjects;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Repositories;
using StockSip.Platform.API.PaymentAndSubscription.Domain.Services;
using StockSip.Platform.API.Shared.Domain.Repositories;

namespace StockSip.Platform.API.PaymentAndSubscription.Application.Internal.CommandService;

public class SubscriptionCommandService(IAccountRepository accountRepository,
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
        
        var latestSubscription = await subscriptionRepository.FindLatestPlanByAccountIdAsync(account.AccountId);

        if (latestSubscription != null && latestSubscription.SubscriptionStatus == ESubscriptionStatus.COMPLETED)
            throw new InvalidOperationException("You already have an active subscription.");
        
        if (plan.PlanType == EPlanType.Free)
        {
            var subscription = new Subscription(account.AccountId, plan);
            subscription.Account = account; 
            subscription.ActivateWithPlan(plan);

            await subscriptionRepository.AddAsync(subscription);
            accountRepository.Update(account);
            await unitOfWork.CompleteAsync();
            
            return "/dashboard";
        }

        var approvalUrl = await paymentService.CreateOrder(
            plan.PlanType.ToString(),
            plan.Price.Amount,
            returnUrl: $"http://localhost:5175/payments-success?accountId={command.AccountId}&planId={command.PlanId}",
            cancelUrl: "http://localhost:5175/payments-cancel"
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

        var subscription = new Subscription(account.AccountId, plan);
        subscription.MarkAsCompleted();
        account.ActiveAccount();

        await subscriptionRepository.AddAsync(subscription);
        accountRepository.Update(account);
        await unitOfWork.CompleteAsync();
    }

    public async Task<string?> Handle(UpgradeSubscriptionCommand command)
    {
        var account = await accountRepository.FindByIdAsync(command.AccountId)
                      ?? throw new Exception("Account not found");

        var upgradePlan = await planRepository.FindByIdAsync(command.PlanId)
                          ?? throw new Exception("Upgrade plan not found");

        if (upgradePlan.PlanType == EPlanType.Free)
            throw new Exception("Cannot upgrade to a free plan");

        var approvalUrl = await paymentService.CreateOrder(
            upgradePlan.PlanType.ToString(),
            upgradePlan.Price.Amount,
            returnUrl: $"http://localhost:5175/payments-upgrade-success?accountId={command.AccountId}&planId={command.PlanId}",
            cancelUrl: "http://localhost:5175/payments-cancel"
        );

        return approvalUrl;
        
    }

    public async Task Handle(CompleteUpgradeCommand command)
    {
        var account = await accountRepository.FindByIdAsync(command.AccountId)
                      ?? throw new Exception("Account not found");

        var plan = await planRepository.FindByIdAsync(command.PlanId)
                   ?? throw new Exception("Plan not found");

        await paymentService.CaptureOrder(command.Token);

        var subscription = await subscriptionRepository.FindLatestPlanByAccountIdAsync(command.AccountId);
    
        if (subscription == null)
        {
            throw new Exception("No active subscription found");
        }

        subscription.UpgradePlan(plan);

        subscriptionRepository.Update(subscription);
        await unitOfWork.CompleteAsync();
    }
}