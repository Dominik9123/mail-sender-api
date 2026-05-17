using MailSender.Application.DTOs;
using MailSender.Application.Interfaces;
using MailSender.Domain.Entities;

namespace MailSender.Application.Services;

public class ClientAppService
{
    private readonly IClientAppRepository _clientAppRepository;
    private readonly ITokenService _tokenService;

    public ClientAppService(
        IClientAppRepository clientAppRepository,
        ITokenService tokenService
    )
    {
        _clientAppRepository = clientAppRepository;
        _tokenService = tokenService;
    }

    public (RegisterClientAppResponse? Response, ClientApp? ExistingClientApp) Register(
        string appId,
        string appName
    )
    {
        // Sprawdzenie czy aplikacja o takim id lub nazwie juz istnieje.
        var existingClientApp = _clientAppRepository.GetByAppIdOrAppName(
            appId,
            appName
        );

        if (existingClientApp != null)
        {
            return (null, existingClientApp);
        }

        var clientApp = new ClientApp
        {
            AppId = appId,
            AppName = appName
        };

        var token = _tokenService.GenerateToken(
            clientApp.AppId,
            clientApp.AppName
        );

        clientApp.Key = token;
        _clientAppRepository.Add(clientApp);

        return (new RegisterClientAppResponse
        {
            AppId = clientApp.AppId,
            AppName = clientApp.AppName,
            Key = token
        }, null);
    }
}
