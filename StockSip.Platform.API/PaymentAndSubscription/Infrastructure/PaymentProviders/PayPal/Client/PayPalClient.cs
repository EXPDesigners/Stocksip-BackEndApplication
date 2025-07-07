using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using StockSip.Platform.API.PaymentAndSubscription.Infrastructure.PaymentProviders.PayPal.Configuration;

namespace StockSip.Platform.API.PaymentAndSubscription.Infrastructure.PaymentProviders.PayPal.Client;

/// <summary>
/// This class is responsible for interacting with the PayPal API to obtain an access token.
/// </summary>
public class PayPalClient
{
    /// <summary>
    /// The HTTP client used to make requests to the PayPal API.
    /// </summary>
    private readonly HttpClient Client;

    /// <summary>
    /// The settings for the PayPal API, including the base URL, client ID, and client secret.
    /// </summary>
    private readonly PayPalSettings Settings;

    /// <summary>
    /// Initializes a new instance of the <see cref="PayPalClient"/> class with the specified options.
    /// </summary>
    /// <param name="options">A configuration options object containing the PayPal settings.</param>
    public PayPalClient(IOptions<PayPalSettings> options)
    {
        Settings = options.Value;
        Client = new HttpClient { BaseAddress = new Uri(Settings.BaseUrl)};
    }

    /// <summary>
    /// This method retrieves an access token from the PayPal API using the client credentials flow.
    /// </summary>
    /// <returns>A string representing the access token.</returns>
    public async Task<string> GetAccessTokenAsync()
    {
        // Create a new HTTP request to the PayPal token endpoint
        var request = new HttpRequestMessage(HttpMethod.Post, "/v1/oauth2/token");

        // Set the request headers for basic authentication and content type
        var byteArray = Encoding.UTF8.GetBytes($"{Settings.ClientId}:{Settings.ClientSecret}");
        
        // Set the Authorization header with the Base64-encoded client ID and secret
        request.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        
        // Set the content type and accept headers for the request
        request.Content = new StringContent("grant_type=client_credentials", Encoding.UTF8, "application/x-www-form-urlencoded");
        
        // Add the Accept header to specify that we want a JSON response
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        // Send the request to the PayPal API and ensure a successful response
        var response = await Client.SendAsync(request);
        
        // Check if the response indicates success; if not, throw an exception
        response.EnsureSuccessStatusCode();
        
        // Read the response content as a string and deserialize it to extract the access token
        var json = await response.Content.ReadAsStringAsync();
        
        // Deserialize the JSON response to a JsonElement and extract the access token
        var obj = JsonSerializer.Deserialize<JsonElement>(json);
        
        // Check if the access token property exists in the JSON response
        return obj.GetProperty("access_token").GetString();
    }

}