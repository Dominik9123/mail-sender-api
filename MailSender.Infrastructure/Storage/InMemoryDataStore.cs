using MailSender.Domain.Entities;

namespace MailSender.Infrastructure.Storage;

//Przechowywanie danych bez bazy
public static class InMemoryDataStore
{
    public static List<ClientApp> ClientApps { get; } = new();

    public static List<MailLog> MailLogs { get; } = new();
}