namespace StockSip.Platform.API.InventoryManagement.Domain.External;

public sealed record AccountDto
(
    string AccountId,
    string Role,
    string BusinessName,
    string Email
);