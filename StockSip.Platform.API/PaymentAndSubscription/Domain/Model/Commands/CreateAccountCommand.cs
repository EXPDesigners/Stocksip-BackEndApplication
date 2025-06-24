namespace StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Commands;

public record CreateAccountCommand(string UserId, string ProfileId, string Role);