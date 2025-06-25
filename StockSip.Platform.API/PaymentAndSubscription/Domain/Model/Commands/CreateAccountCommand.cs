namespace StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Commands;

/// <summary>
/// This command is used to create a new account in the payment and subscription domain.
/// </summary>
/// <param name="username">The username for the new user.</param>
/// <param name="password">The password for the new user.</param>
/// <param name="role">The role assigned to the new account.</param>
public record CreateAccountCommand(string username, string password, string role);