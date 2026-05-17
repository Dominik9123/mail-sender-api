namespace MailSender.Application.Interfaces;

public interface ITokenService
{
    string GenerateToken(string appId, string appName);
}
