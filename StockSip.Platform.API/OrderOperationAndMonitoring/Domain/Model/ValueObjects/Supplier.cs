namespace StockSip.Platform.API.OrderOperationAndMonitoring.Domain.Model.ValueObjects;

public record Supplier(
    string AccountId,
    string UserOwnerId,
    string Role,
    string BusinessName,
    string Email);