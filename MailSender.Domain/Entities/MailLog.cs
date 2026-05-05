namespace MailSender.Domain.Entities;

public class MailLog
{
    public Guid Id { get; set; }

    public Guid ClientAppId { get; set; } // Mowi ktora aplikacja wyslala maila

    public string Recipient { get; set; } = string.Empty; // Adres odbiorcy

    public string Subject { get; set; } = string.Empty; // Temat Maila

    public string Body { get; set; } = string.Empty; // Tresc Maila

    public bool IsSuccess { get; set; } // Czy wyslanie sie udalo

    public string? ErrorMessage { get; set; } // Tresc Bledu

    public DateTime SentAt { get; set; } = DateTime.UtcNow; // Data Proby wysylki

}

