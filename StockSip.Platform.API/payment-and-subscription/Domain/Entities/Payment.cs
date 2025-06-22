using StockSip.Platform.API.payment_and_subscription.domain.ValueObjects;
using StockSip.Platform.API.Shared.Domain.Model.ValueObjects;
using DateTime = System.DateTime;

namespace StockSip.Platform.API.payment_and_subscription.Domain.Entities;

/// <summary>
/// This entity represents a payment in the payment and subscription domain.
/// </summary>
public class Payment
{
    public string PaymentId { get; set; } = Guid.NewGuid().ToString();
    
    public string AccountId { get; set; }
    
    public Money Amount { get; set; }
    
    public EStatus Status { get; set; }
    
    public DateTime PaidDate { get; set; }
    
    /// <summary>
    /// Default constructor for EF Core.
    /// </summary>
    private Payment() {}
}