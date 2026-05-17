using MailSender.Domain.Entities;

namespace MailSender.Application.Interfaces;

public interface IClientAppRepository
{
    void Add(ClientApp clientApp);

    ClientApp? GetByAppId(string appId);

    ClientApp? GetByAppIdOrAppName(string appId, string appName);
}