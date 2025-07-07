using System.Text.Json.Serialization;

namespace StockSip.Platform.API.InventoryManagement.Domain.External;

public sealed record AccountDto
(
    [property: JsonPropertyName("accountId")]
    string AccountId,
    
    [property: JsonPropertyName("accountRole")]
    string Role,

    [property: JsonPropertyName("businessName")]
    string BusinessName,

    string Email
);