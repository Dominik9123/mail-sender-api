using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace MailSender.Infrastructure.Services;

public class JwtTokenService
{
    private const string SecretKey = "SuperSecretKeyForMailSenderProject123456!"; // Klucz podpisujacy JWT
    private readonly byte[] _key = Encoding.UTF8.GetBytes(SecretKey);

    public string GenerateToken(Guid clientAppId, string email)
    {
        var claims = new[] //Przechowywanie danych w tokenie: ID aplikacji, email
        {
            new Claim("ClientAppId", clientAppId.ToString()),
            new Claim(ClaimTypes.Email, email)
        };

        // Podpis tokena za pomoca klucza symetrycznego
        var credentials = new SigningCredentials(
           new SymmetricSecurityKey(_key),
           SecurityAlgorithms.HmacSha256 //Algorytm podpisu
       );

        var token = new JwtSecurityToken( //token wazny 7 dni
            claims: claims,
            expires: DateTime.UtcNow.AddDays(7),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
