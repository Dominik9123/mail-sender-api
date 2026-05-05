using MailSender.Application.Interfaces;

namespace MailSender.Infrastructure.Providers;

//Symulacja Wysylki
public class FakeMailSenderProvider : IMailSenderProvider
{
    public async Task<bool> SendEmailAsync(
        string recipient,
        string subject,
        string body
    )
    {
        await Task.Delay(500);

        Console.WriteLine($"Sending mail to: {recipient}");
        Console.WriteLine($"Subject: {subject}");
        Console.WriteLine($"Body: {body}");

        return true;
    }
}