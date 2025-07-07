namespace StockSip.Platform.API.Authorization.Application.Internal.OutboundServices.Email;

/// <summary>
/// This interface defines the contract for an email service that sends recovery emails.
/// </summary>
public interface IEmailService
{
    /// <summary>
    /// This method sends a recovery email to the specified email address with a recovery link.
    /// </summary>
    /// <param name="toEmail">The email address to send the recovery email to</param>
    /// <param name="code">The recovery code to include in the email</param>
    /// <returns>A task representing the asynchronous operation</returns>
    Task SendPasswordRecoveryEmail(string toEmail, string code);
}