using System.Net;
using System.Net.Mail;
using Users.Worker.Application.Emails;

namespace Users.Worker.Infrastructure.Emails.Services;

public sealed class EmailService(EmailSettings settings) : IEmailService
{
    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var client = new SmtpClient(settings.Host, settings.Port)
        {
            EnableSsl = true,
            Credentials = new NetworkCredential(settings.Email, settings.Password)
        };

        await client.SendMailAsync(
            new MailMessage(
                    from: settings.Email,
                    to: to,
                    subject: subject,
                    body: body
            ));
    }
}
