using MailSender.Domain.Entities;

namespace MailSender.Application.Interfaces;

public interface IMailLogRepository
{
    void Add(MailLog mailLog);

    IReadOnlyCollection<MailLog> GetByAppId(string appId);

    MailLog? GetByIdForApp(Guid id, string appId);
}
