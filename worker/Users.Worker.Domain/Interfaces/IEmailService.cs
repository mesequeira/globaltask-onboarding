namespace Users.Worker.Application.Emails;

public interface IEmailService
{
    Task SendEmailAsync(string to, string subject, string body);
}
