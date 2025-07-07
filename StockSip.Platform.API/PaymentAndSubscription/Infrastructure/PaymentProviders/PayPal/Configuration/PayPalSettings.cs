namespace StockSip.Platform.API.PaymentAndSubscription.Infrastructure.PaymentProviders.PayPal.Configuration;

/// <summary>
/// This class holds the configuration settings for PayPal integration.
/// </summary>
public class PayPalSettings
{
    /// <summary>
    /// PayPal client ID.
    /// </summary>
    public string ClientId { get; set; } = null;

    /// <summary>
    /// PayPal client secret.
    /// </summary>
    public string ClientSecret { get; set; } = null;

    /// <summary>
    /// PayPal environment URL (sandbox or live).
    /// </summary>
    public string BaseUrl { get; set; } = null;
}