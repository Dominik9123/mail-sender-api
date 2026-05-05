namespace MailSender.Application.Interfaces;

public interface IMailSenderProvider
{
    Task<bool> SendEmailAsync(
        string recipient,
        string subject,
        string body
    );
}