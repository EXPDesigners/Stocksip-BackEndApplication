namespace StockSip.Platform.API.PaymentAndSubscription.Domain.Model.Commands;

public record SignUpWithAccountCommand(string Username, 
                                       string Password, 
                                       string ValidatePassword, 
                                       string AccountRole, 
                                       string BusinessName);