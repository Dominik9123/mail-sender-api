namespace MailSender.Application.DTOs;

//Zwracanie ID aplikacji / JWT 
public class RegisterClientAppResponse
{
    public Guid Id { get; set; }

    public string Token { get; set; } = string.Empty;

}