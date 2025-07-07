namespace StockSip.Platform.API.PaymentAndSubscription.Application.Internal.OutboundServices.PayPal;

/// <summary>
/// This interface defines the contract for payment services using PayPal.
/// </summary>
public interface IPaymentService
{
    /// <summary>
    /// Defines a method to create an order with PayPal.
    /// </summary>
    /// <param name="planName">The name of the subscription plan.</param>
    /// <param name="amount">The amount to be charged for the subscription.</param>
    /// <param name="returnUrl">The URL to redirect to after a successful payment.</param>
    /// <param name="cancelUrl">A URL to redirect to if the payment is cancelled.</param>
    /// <returns></returns>
    Task<string> CreateOrder(string planName, decimal amount, string returnUrl, string cancelUrl);
    
    /// <summary>
    /// This method captures the order using the provided token.
    /// </summary>
    /// <param name="token">A token representing the order to be captured.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task CaptureOrder(string token);
}