using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MailSender.Application.DTOs;
using MailSender.Application.Interfaces;
using MailSender.Domain.Entities;
using MailSender.Infrastructure.Storage;

namespace MailSender.Api.Controllers;

[ApiController]
[Route("mail")]
public class MailController : ControllerBase
{
    private readonly IMailSenderProvider _mailSenderProvider;

    public MailController(IMailSenderProvider mailSenderProvider)
    {
        // Provider wstrzykiwany przez Dependency Injection (mozna latwo podmienic implementacje)
        _mailSenderProvider = mailSenderProvider;
    }

    [Authorize] // Endpoint dostepny tylko z poprawnym tokenem JWT
    [HttpPost("send")]
    public async Task<ActionResult<SendMailResponse>> SendMail(
        SendMailRequest request
    )
    {
        // Pobranie ID aplikacji z tokena JWT
        var clientAppId = User.FindFirstValue("ClientAppId");

        if (string.IsNullOrWhiteSpace(clientAppId))
        {
            return Unauthorized("Invalid token.");
        }

        // Podstawowa walidacja danych wejsciowych.
        if (string.IsNullOrWhiteSpace(request.Recipient) ||
            string.IsNullOrWhiteSpace(request.Subject) ||
            string.IsNullOrWhiteSpace(request.Body))
        {
            return BadRequest("Recipient, subject and body are required.");
        }

        // Wyslanie maila przez aktualnie skonfigurowanego providera
        var isSent = await _mailSenderProvider.SendEmailAsync(
            request.Recipient,
            request.Subject,
            request.Body
        );

        // Zapis logu wysylki (symulacja bazy danych)
        var mailLog = new MailLog
        {
            Id = Guid.NewGuid(), // unikalne id logu
            ClientAppId = Guid.Parse(clientAppId), //zamienia ID pobrane z tokena JWT ze stringa na Guid
            Recipient = request.Recipient,
            Subject = request.Subject,
            Body = request.Body,
            IsSuccess = isSent, //zapis czy provider zwrocil sukces
            ErrorMessage = isSent ? null : "Mail sending failed.",
            SentAt = DateTime.UtcNow
        };

        InMemoryDataStore.MailLogs.Add(mailLog);

        if (!isSent)
        {
            return StatusCode(500, new SendMailResponse
            {
                Success = false,
                Message = "Mail sending failed."
            });
        }

        return Ok(new SendMailResponse
        {
            Success = true,
            Message = "Mail sent successfully."
        });
    }
}