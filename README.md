# MailSender API

Projekt backendowy w .NET umożliwiający wysyłkę maili przez API z wykorzystaniem JWT do autoryzacji.

## Funkcjonalności

- Rejestracja aplikacji klienckiej (`/client-app/register`)
- Generowanie i weryfikacja tokenów JWT
- Wysyłanie maili (`/mail/send`)
- Obsługa wielu providerów (Fake + Console) przez interfejs i DI
- Logowanie wysyłki maili
- Pobieranie logów (`/mail-log`, `/mail-log/{id}`)

## Technologie

- ASP.NET Core Web API
- JWT Authentication
- Dependency Injection
- Clean Architecture
- Swagger UI

## Uruchomienie

```bash
dotnet run --project MailSender.Api
```

## Swagger

Po uruchomieniu aplikacji dokumentacja API dostępna jest pod adresem:

http://localhost:5215/swagger

## Testowanie

### Rejestracja aplikacji

```json
{
  "name": "Test Client App",
  "email": "test@example.com",
  "registrationPassword": "q#w@85"
}
```

### Autoryzacja

Po rejestracji skopiuj token JWT i wklej w Swaggerze:

Bearer TOKEN

### Wysyłka maila

```json
{
  "recipient": "receiver@example.com",
  "subject": "Test message",
  "body": "Hello from MailSender!"
}
```

## Bezpieczeństwo

Endpointy związane z wysyłką maili oraz logami są zabezpieczone tokenem JWT.  
Każda aplikacja widzi tylko swoje logi dzięki filtrowaniu po `ClientAppId`.

## Struktura projektu

- MailSender.Api
- MailSender.Application
- MailSender.Domain
- MailSender.Infrastructure
- MailSender.Tests
