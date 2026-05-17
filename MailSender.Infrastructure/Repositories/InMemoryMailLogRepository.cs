using MailSender.Application.Interfaces;
using MailSender.Domain.Entities;
using MailSender.Infrastructure.Storage;

namespace MailSender.Infrastructure.Repositories;

public class InMemoryMailLogRepository : IMailLogRepository
{
    public void Add(MailLog mailLog)
    {
        InMemoryDataStore.MailLogs.Add(mailLog);
    }

    public IReadOnlyCollection<MailLog> GetByAppId(string appId)
    {
        return InMemoryDataStore.MailLogs
            .Where(x => x.AppId == appId)
            .ToList();
    }

    public MailLog? GetByIdForApp(Guid id, string appId)
    {
        return InMemoryDataStore.MailLogs
            .FirstOrDefault(x => x.Id == id && x.AppId == appId);
    }
}
