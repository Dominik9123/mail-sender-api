using MailSender.Application.Interfaces;
using MailSender.Domain.Entities;
using MailSender.Infrastructure.Storage;

namespace MailSender.Infrastructure.Repositories;

public class InMemoryClientAppRepository : IClientAppRepository
{
    public void Add(ClientApp clientApp)
    {
        InMemoryDataStore.ClientApps.Add(clientApp);
    }

    public ClientApp? GetByAppId(string appId)
    {
        return InMemoryDataStore.ClientApps
            .FirstOrDefault(x => x.AppId == appId);
    }

    public ClientApp? GetByAppIdOrAppName(string appId, string appName)
    {
        return InMemoryDataStore.ClientApps
            .FirstOrDefault(x =>
                x.AppId == appId ||
                x.AppName == appName
                );
    }
}
