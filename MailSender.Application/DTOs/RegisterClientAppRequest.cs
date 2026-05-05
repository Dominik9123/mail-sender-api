namespace MailSender.Application.DTOs;


// Model danych wejsciowych przy rejestracji aplikacji
public class RegisterClientAppRequest
{
    public string Name { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string RegistrationPassword { get; set; } = string.Empty;
}