namespace MailSender.Application.DTOs;

public class SendMailResponse
{
    public bool Success { get; set; }

    public string Message { get; set; } = string.Empty;
}