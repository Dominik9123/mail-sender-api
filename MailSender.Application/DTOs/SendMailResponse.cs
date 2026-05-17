namespace MailSender.Application.DTOs;

public class SendMailResponse
{
    public string AppId { get; set; } = string.Empty;
    public string AppName { get; set; } = string.Empty;
    public string Status { get; set; } = "queued";
    public SendMailRequest Email { get; set; } = new();
}
