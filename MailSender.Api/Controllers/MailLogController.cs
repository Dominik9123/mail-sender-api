using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MailSender.Application.Services;
using MailSender.Domain.Entities;

namespace MailSender.Api.Controllers;

[ApiController]
[Route("mail-log")]
public class MailLogController : ControllerBase
{
    private readonly MailLogService _mailLogService;

    public MailLogController(MailLogService mailLogService)
    {
        _mailLogService = mailLogService;
    }

    [Authorize]
    [HttpGet]
    public ActionResult<List<MailLog>> GetLogs()
    {
        // Logi sa filtrowane wedlug aplikacji z tokena.
        var appId = User.FindFirstValue("AppId");

        if (string.IsNullOrWhiteSpace(appId))
        {
            return Unauthorized("Invalid token.");
        }

        var logs = _mailLogService.GetByAppId(appId);

        return Ok(logs);
    }

    [Authorize]
    [HttpGet("{id}")]
    public ActionResult<MailLog> GetLogById(Guid id)
    {
        // Logi sa filtrowane wedlug aplikacji z tokena.
        var appId = User.FindFirstValue("AppId");

        if (string.IsNullOrWhiteSpace(appId))
        {
            return Unauthorized("Invalid token.");
        }

        var log = _mailLogService.GetByIdForApp(id, appId);

        if (log == null)
        {
            return NotFound("Mail log not found.");
        }

        return Ok(log);
    }
}
