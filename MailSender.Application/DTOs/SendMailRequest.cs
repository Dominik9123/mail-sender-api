namespace MailSender.Application.DTOs;

//Model requestu dla /mail/send
public class SendMailRequest
{
    public string Recipient { get; set; } = string.Empty;

    public string Subject { get; set; } = string.Empty;

    public string Body { get; set; } = string.Empty;
}