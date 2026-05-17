using System.Text.RegularExpressions;
using MailSender.Application.DTOs;
using MailSender.Application.Interfaces;
using MailSender.Domain.Entities;

namespace MailSender.Application.Services;

public class MailService
{
    private const string StudentSurname = "Skutecki";
    private readonly IMailSenderProvider _mailSenderProvider;
    private readonly IClientAppRepository _clientAppRepository;
    private readonly IMailLogRepository _mailLogRepository;

    public MailService(
        IMailSenderProvider mailSenderProvider,
        IClientAppRepository clientAppRepository,
        IMailLogRepository mailLogRepository
    )
    {
        _mailSenderProvider = mailSenderProvider;
        _clientAppRepository = clientAppRepository;
        _mailLogRepository = mailLogRepository;
    }

    public async Task<SendMailResponse?> SendAsync(
        string appId,
        SendMailRequest request
    )
    {
        var clientApp = _clientAppRepository.GetByAppId(appId);

        if (clientApp == null)
        {
            return null;
        }

        var subject = request.Subject;
        var body = request.Body;

        // Zastosowanie regul biznesowych przed wysylka wiadomosci.
        if (subject.TrimEnd().EndsWith("?"))
        {
            subject = "[Q] " + subject;
        }

        body = Regex.Replace(
            body,
            Regex.Escape(StudentSurname),
            "[student.name]$0[/student.surname]",
            RegexOptions.IgnoreCase
        );

        var isSent = false;
        string? errorMessage = null;

        try
        {
            isSent = await _mailSenderProvider.SendEmailAsync(
                request.To,
                subject,
                body
            );

            if (!isSent)
            {
                errorMessage = "Mail sending failed.";
            }
        }
        catch (Exception ex)
        {
            errorMessage = ex.Message;
        }

        var mailLog = new MailLog
        {
            Id = Guid.NewGuid(),
            AppId = appId,
            To = request.To,
            Subject = subject,
            Body = body,
            IsSuccess = isSent,
            ErrorMessage = errorMessage,
            SentAt = DateTime.UtcNow
        };

        _mailLogRepository.Add(mailLog);

        return new SendMailResponse
        {
            AppId = clientApp.AppId,
            AppName = clientApp.AppName,
            Status = isSent ? "queued" : "failed",
            Email = new SendMailRequest
            {
                To = request.To,
                Subject = subject,
                Body = body
            }
        };
    }
}
