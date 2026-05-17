namespace MailSender.Application.DTOs;


// Model danych wejsciowych przy rejestracji aplikacji
public class RegisterClientAppRequest
{
    public string AppId { get; set; } = string.Empty;

    public string AppName { get; set; } = string.Empty;

    public string Pass { get; set; } = string.Empty;
}