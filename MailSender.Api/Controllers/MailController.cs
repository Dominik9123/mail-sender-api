using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MailSender.Application.DTOs;
using MailSender.Application.Services;

namespace MailSender.Api.Controllers;

[ApiController]
[Route("mail")]
public class MailController : ControllerBase
{
    private readonly MailService _mailService;

    public MailController(MailService mailService)
    {
        _mailService = mailService;
    }

    [Authorize]
    [HttpPost("send")]
    public async Task<ActionResult<SendMailResponse>> SendMail(
        SendMailRequest request
    )
    {
        // Id aplikacji jest przechowywane w tokenie JWT.
        var appId = User.FindFirstValue("AppId");

        if (string.IsNullOrWhiteSpace(appId))
        {
            return Unauthorized("Invalid token.");
        }

        // Podstawowa walidacja danych przed wysylka.
        if (string.IsNullOrWhiteSpace(request.To) ||
            string.IsNullOrWhiteSpace(request.Subject) ||
            string.IsNullOrWhiteSpace(request.Body))
        {
            return BadRequest("To, subject and body are required.");
        }

        var response = await _mailService.SendAsync(appId, request);

        if (response == null)
        {
            return Unauthorized("Client application not found.");
        }

        if (response.Status == "failed")
        {
            return StatusCode(500, response);
        }

        return Ok(response);
    }
}
