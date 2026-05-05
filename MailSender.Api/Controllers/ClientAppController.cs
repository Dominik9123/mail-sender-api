using Microsoft.AspNetCore.Mvc;
using MailSender.Application.DTOs;
using MailSender.Domain.Entities;
using MailSender.Infrastructure.Services;
using MailSender.Infrastructure.Storage;

namespace MailSender.Api.Controllers;

[ApiController]
[Route("client-app")] // Bazowy adres dla endpointow klienta
public class ClientAppController : ControllerBase
{
    private readonly JwtTokenService _jwtTokenService; // Serwis do generowania tokenow JWT

    public ClientAppController()
    {
        // Inicjalizacja serwisu odpowiedzialnego za generacje tokenow JWT
        _jwtTokenService = new JwtTokenService();
    }

    [HttpPost("register")]
    public ActionResult<RegisterClientAppResponse> Register(
        RegisterClientAppRequest request
    )
    {

        if (request.RegistrationPassword != "q#w@85") // Sprawdzenie poprawnosci hasla rejestracyjnego zgodnie z wymaganiami projektu - 2 ostatnie cyfry indeksu

        {
            return Unauthorized("Invalid registration password.");
        }

        // Utworzenie nowej aplikacji-klienta
        var clientApp = new ClientApp
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Email = request.Email
        };

        // Generacja tokena JWT na podstawie ID i emaila
        var token = _jwtTokenService.GenerateToken(
            clientApp.Id,
            clientApp.Email
        );
        // Przypisanie tokena do aplikacji
        clientApp.Token = token;

        // Zapisanie aplikacji w pamieci (symulacja bazy danych)
        InMemoryDataStore.ClientApps.Add(clientApp);

        // Zwrocenie ID aplikacji oraz tokena JWT
        return Ok(new RegisterClientAppResponse
        {
            Id = clientApp.Id,
            Token = token

        });
    }
}