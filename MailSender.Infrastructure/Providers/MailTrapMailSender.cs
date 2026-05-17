using System.Net.Http.Json;
using MailSender.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace MailSender.Infrastructure.Providers;

public class MailTrapMailSender : IMailSenderProvider
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public MailTrapMailSender(
        HttpClient httpClient,
        IConfiguration configuration
    )
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<bool> SendEmailAsync(
        string recipient,
        string subject,
        string body
    )
    {
        var apiKey = _configuration["Mailtrap:ApiKey"];
        var senderEmail = _configuration["Mailtrap:SenderEmail"];
        var senderName = _configuration["Mailtrap:SenderName"] ?? "MailSender";

        // Dane dostepowe sa pobierane z konfiguracji, nie z kodu.
        if (string.IsNullOrWhiteSpace(apiKey) ||
             string.IsNullOrWhiteSpace(senderEmail))
        {
            throw new InvalidOperationException("Missing Mailtrap configuration.");
        }

        var requestBody = new
        {
            from = new
            {
                email = senderEmail,
                name = senderName
            },
            to = new[]
            {
                new
                {
                    email = recipient
                }
            },
            subject = subject,
            text = body
        };

        using var request = new HttpRequestMessage(
            HttpMethod.Post,
            "https://send.api.mailtrap.io/api/send"
        );

        request.Headers.Authorization =
            new System.Net.Http.Headers.AuthenticationHeaderValue(
                "Bearer",
                apiKey
            );

        request.Content = JsonContent.Create(requestBody);

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Mailtrap error: {error}");
            return false;
        }

        return true;
    }
}
