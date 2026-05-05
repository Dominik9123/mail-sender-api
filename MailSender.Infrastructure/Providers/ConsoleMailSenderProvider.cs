using MailSender.Application.Interfaces;

namespace MailSender.Infrastructure.Providers;

public class ConsoleMailSenderProvider : IMailSenderProvider
{
    public Task<bool> SendEmailAsync(
        string recipient,
        string subject,
        string body
    )
    {

        //Drugi Provider wypisuje tresc wiadomosci w konsoli.
        Console.WriteLine("=== Console Mail Provider ===");
        Console.WriteLine($"Recipient: {recipient}");
        Console.WriteLine($"Subject: {subject}");
        Console.WriteLine($"Body: {body}");
        Console.WriteLine("=============================");

        return Task.FromResult(true);
    }
}