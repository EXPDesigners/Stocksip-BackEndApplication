namespace StockSip.Platform.API.OrderOperationAndMonitoring.Interfaces.REST.Resources;

public record AccountResource(
    string AccountId,
    string UserOwnerId,
    string Role,
    string BusinessName,
    string Email);