using Microsoft.AspNetCore.Mvc;
using MailSender.Application.DTOs;
using MailSender.Application.Services;

namespace MailSender.Api.Controllers;

[ApiController]
[Route("client-app")]
public class ClientAppController : ControllerBase
{
    private readonly ClientAppService _clientAppService;

    public ClientAppController(ClientAppService clientAppService)
    {
        _clientAppService = clientAppService;
    }

    [HttpPost("register")]
    public ActionResult<RegisterClientAppResponse> Register(
        RegisterClientAppRequest request
    )
    {

        // Haslo wymagane do rejestracji aplikacji klienckiej.
        if (request.Pass != "q##waQ85")
        {
            return StatusCode(StatusCodes.Status403Forbidden, new
            {
                error = "Invalid index-based password 85"
            });
        }
        var result = _clientAppService.Register(
            request.AppId,
            request.AppName
        );

        if (result.ExistingClientApp != null)
        {
            return Conflict(new
            {
                error = $"Client app duplication. Existing {result.ExistingClientApp.AppId} {result.ExistingClientApp.AppName}"
            });
        }

        return Ok(result.Response);
    }
}
