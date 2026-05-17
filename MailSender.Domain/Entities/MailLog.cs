namespace MailSender.Domain.Entities;

public class MailLog
{
    public Guid Id { get; set; }

    public string AppId { get; set; } = string.Empty;

    public string To { get; set; } = string.Empty;

    public string Subject { get; set; } = string.Empty;

    public string Body { get; set; } = string.Empty;

    public bool IsSuccess { get; set; }

    public string? ErrorMessage { get; set; }

    public DateTime SentAt { get; set; } = DateTime.UtcNow;

}

