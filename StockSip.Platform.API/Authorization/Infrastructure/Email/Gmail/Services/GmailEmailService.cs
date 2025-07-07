using StockSip.Platform.API.Authorization.Application.Internal.OutboundServices.Email;

using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Options;
using StockSip.Platform.API.Authorization.Infrastructure.Email.Gmail.Configuration;

namespace StockSip.Platform.API.Authorization.Infrastructure.Email.Gmail.Services;

public class GmailEmailService : IEmailService
{
    private readonly EmailSettings Settings;

    public GmailEmailService(IOptions<EmailSettings> settings)
    {
        Settings = settings.Value;
    }
    
    public async Task SendPasswordRecoveryEmail(string toEmail, string code)
    {
        var mail = new MailMessage
        {
            From = new MailAddress(Settings.From),
            Subject = "Código de recuperación de contraseña",
            Body = $@"
            <p>Tu código de recuperación es:</p>
            <h2 style='color: #790b38;'>{code}</h2>
            <p>Este código es válido por solo unos minutos.</p>",
            IsBodyHtml = true
        };

        mail.To.Add(toEmail);

        using var smtp = new SmtpClient(Settings.SmtpServer, Settings.Port)
        {
            Credentials = new NetworkCredential(Settings.From, Settings.Password),
            EnableSsl = true
        };

        await smtp.SendMailAsync(mail);
    }
}