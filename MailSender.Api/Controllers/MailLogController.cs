using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MailSender.Domain.Entities;
using MailSender.Infrastructure.Storage;

namespace MailSender.Api.Controllers;

[ApiController]
[Route("mail-log")]
public class MailLogController : ControllerBase
{
    [Authorize]  //Widocznosc tylko dla zalogowanego usera
    [HttpGet]
    public ActionResult<List<MailLog>> GetLogs()
    {
        //Pobranie ClientAppId z tokena
        var clientAppId = User.FindFirstValue("ClientAppId"); //pobranie id apki z tokena

        if (string.IsNullOrWhiteSpace(clientAppId))
        {
            return Unauthorized("Invalid token.");
        }

        var clientId = Guid.Parse(clientAppId);

        //Filtrowanie logow tylko dla danej aplikacji

        var logs = InMemoryDataStore.MailLogs
            .Where(x => x.ClientAppId == clientId) //tylko logi danej aplikacji
            .ToList();

        return Ok(logs);
    }

    [Authorize]
    [HttpGet("{id}")]
    public ActionResult<MailLog> GetLogById(Guid id)
    {
        //Pobranie ClientAppId z tokena JWT
        var clientAppId = User.FindFirstValue("ClientAppId");

        if (string.IsNullOrWhiteSpace(clientAppId))
        {
            return Unauthorized("Invalid token.");
        }

        var clientId = Guid.Parse(clientAppId);

        //Szukanie konkretnego logu nalezacego do aktualnej aplikacji
        var log = InMemoryDataStore.MailLogs
            .FirstOrDefault(x => x.Id == id && x.ClientAppId == clientId); //Sprawdzamy 2 rzeczy czy log istnieje oraz czy nalezy do aplikacji z tokena

        if (log == null)
        {
            return NotFound("Mail log not found.");
        }

        return Ok(log);
    }
}
