using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MailSender.Application.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace MailSender.Infrastructure.Services;

public class JwtTokenService : ITokenService
{
    private readonly byte[] _key;

    public JwtTokenService(string secretKey)
    {
        if (string.IsNullOrWhiteSpace(secretKey))
        {
            throw new ArgumentException("JWT secret cannot be empty.", nameof(secretKey));
        }

        _key = Encoding.UTF8.GetBytes(secretKey);
    }

    public string GenerateToken(string appId, string appName)
    {
        // Dane aplikacji zapisywane w tokenie.
        var claims = new[]
        {
            new Claim("AppId", appId),
            new Claim("AppName", appName)
        };

        // Podpis tokena za pomoca klucza symetrycznego.
        var credentials = new SigningCredentials(
           new SymmetricSecurityKey(_key),
           SecurityAlgorithms.HmacSha256
       );

        // Token jest wazny przez 90 dni.
        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddDays(90),
            signingCredentials: credentials
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
