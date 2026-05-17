using MailSender.Application.Interfaces;
using MailSender.Domain.Entities;

namespace MailSender.Application.Services;

public class MailLogService
{
    private readonly IMailLogRepository _mailLogRepository;

    public MailLogService(IMailLogRepository mailLogRepository)
    {
        _mailLogRepository = mailLogRepository;
    }

    public IReadOnlyCollection<MailLog> GetByAppId(string appId)
    {
        return _mailLogRepository.GetByAppId(appId);
    }

    public MailLog? GetByIdForApp(Guid id, string appId)
    {
        return _mailLogRepository.GetByIdForApp(id, appId);
    }
}
