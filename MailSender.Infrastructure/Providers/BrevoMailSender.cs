using System.Net.Http.Json;
using MailSender.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace MailSender.Infrastructure.Providers;

public class BrevoMailSender : IMailSenderProvider
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public BrevoMailSender(
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
        var apiKey = _configuration["Brevo:ApiKey"];
        var senderEmail = _configuration["Brevo:SenderEmail"];
        var senderName = _configuration["Brevo:SenderName"] ?? "MailSender";

        // Dane dostepowe sa pobierane z konfiguracji, nie z kodu.
        if (string.IsNullOrWhiteSpace(apiKey) ||
            string.IsNullOrWhiteSpace(senderEmail))
        {
            throw new InvalidOperationException("Missing Brevo configuration.");
        }

        var requestBody = new
        {
            sender = new
            {
                name = senderName,
                email = senderEmail
            },
            to = new[]
            {
                new
                {
                    email = recipient
                }
            },
            subject = subject,
            textContent = body
        };

        using var request = new HttpRequestMessage(
            HttpMethod.Post,
            "https://api.brevo.com/v3/smtp/email"
        );

        request.Headers.Add("api-key", apiKey);
        request.Headers.Add("accept", "application/json");
        request.Content = JsonContent.Create(requestBody);

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Brevo error: {error}");
            return false;
        }

        return true;
    }
}
