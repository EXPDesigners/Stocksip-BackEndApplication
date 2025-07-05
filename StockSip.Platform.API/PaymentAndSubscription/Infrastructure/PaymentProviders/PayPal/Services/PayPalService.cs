using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using StockSip.Platform.API.PaymentAndSubscription.Application.Internal.OutboundServices.PayPal;
using StockSip.Platform.API.PaymentAndSubscription.Infrastructure.PaymentProviders.PayPal.Client;
using StockSip.Platform.API.PaymentAndSubscription.Infrastructure.PaymentProviders.PayPal.Configuration;

namespace StockSip.Platform.API.PaymentAndSubscription.Infrastructure.PaymentProviders.PayPal.Services;

/// <summary>
/// The PayPalService class provides methods to create and capture PayPal orders.
/// </summary>
public class PayPalService : IPaymentService
{
    /// <summary>
    /// The PayPal client used to interact with the PayPal API.
    /// </summary>
    private readonly PayPalClient Client;
    
    /// <summary>
    /// Defines the settings for the PayPal service, including API credentials and base URL.
    /// </summary>
    private readonly PayPalSettings Settings;
    
    /// <summary>
    /// A constructor that initializes the PayPalService with a PayPalClient and PayPalSettings.
    /// </summary>
    /// <param name="client">The PayPal client used to interact with the PayPal API.</param>
    /// <param name="options">The options containing the PayPal settings, including API credentials and base URL.</param>
    public PayPalService(PayPalClient client, IOptions<PayPalSettings> options)
    {
        Client = client;
        Settings = options.Value;
    }
    
    /// <summary>
    /// This method creates a PayPal order for a specified plan with a given amount.
    /// </summary>
    /// <param name="planName">The name of the plan for which the order is being created.</param>
    /// <param name="amount">The amount to be charged for the order.</param>
    /// <param name="returnUrl">The URL to which the user will be redirected after completing the payment.</param>
    /// <param name="cancelUrl">The URL to which the user will be redirected if they cancel the payment.</param>
    /// <returns>A task that represents the asynchronous operation, containing the approval URL for the PayPal order.</returns>
    public async Task<string> CreateOrder(string planName, decimal amount, string returnUrl, string cancelUrl)
    {
        // Ensure the amount is a valid decimal with two decimal places
        var token = await Client.GetAccessTokenAsync();

        // Create the order body with the specified plan name and amount
        var body = new
        {
            intent = "CAPTURE",
            purchase_units = new[]
            {
                new { amount = new { currency_code = "USD", value = amount.ToString("F2") }, description = $"Plan: {planName}" }
            },
            application_context = new { return_url = returnUrl, cancel_url = cancelUrl }
        };

        // Create the HTTP request to the PayPal API to create the order
        var request = new HttpRequestMessage(HttpMethod.Post, "/v2/checkout/orders");
        
        // Set the request headers and content
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        
        // Set the request headers to accept JSON responses
        request.Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");

        // Set the request headers to accept JSON responses
        var client = new HttpClient { BaseAddress = new Uri(Settings.BaseUrl) };
        
        // Add the Accept header to specify that we want a JSON response
        var response = await client.SendAsync(request);
        
        // Ensure the response indicates success
        response.EnsureSuccessStatusCode();

        // Read the response content and deserialize it to get the order details
        var responseBody = await response.Content.ReadAsStringAsync();
        
        // Deserialize the response body to a JsonElement
        var order = JsonSerializer.Deserialize<JsonElement>(responseBody);

        // Find the approval URL from the order links
        return order.GetProperty("links").EnumerateArray()
            .First(l => l.GetProperty("rel").GetString() == "approve")
            .GetProperty("href").GetString();
    }

    /// <summary>
    /// This method captures a PayPal order using the provided token.
    /// </summary>
    /// <param name="token">The token representing the PayPal order to be captured.</param>
    public async Task CaptureOrder(string token)
    {
        // Ensure the token is not null or empty
        var accessToken = await Client.GetAccessTokenAsync();
        
        // Create the HTTP client and set the authorization header with the access token
        var client = new HttpClient();
        
        // Set the base address for the PayPal API
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        // Create the HTTP request to capture the order
        var request = new HttpRequestMessage(HttpMethod.Post, $"{Settings.BaseUrl}/v2/checkout/orders/{token}/capture");
        
        // Set the request headers to accept JSON responses
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        
        // Set the request content to an empty JSON object
        request.Content = new StringContent("{}", Encoding.UTF8, "application/json");

        // Send the request to the PayPal API to capture the order
        var response = await client.SendAsync(request);
        
        // Ensure the response indicates success
        response.EnsureSuccessStatusCode();

        // Read the response content and print it to the console
        var json = await response.Content.ReadAsStringAsync();
        Console.WriteLine("Capture response: " + json);
    }
}