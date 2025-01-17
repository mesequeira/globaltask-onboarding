namespace Users.Worker.Domain.Abstractions;

public class EmailMessage(string subject, string body)
{
    public string Subject { get; set; } = subject;
    public string Body { get; set; } = body;
}