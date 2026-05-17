# MailSender

Dokumentacja `README.md` została przygotowana z pomocą Codexa.

`MailSender` to aplikacja Web API w .NET służąca do rejestrowania aplikacji klienckich i wysyłania wiadomości e-mail przez konfigurowalnych providerów.

## Funkcjonalności

- rejestracja aplikacji klienckiej i generowanie tokenu JWT
- autentykacja JWT dla chronionych endpointów
- wysyłanie wiadomości przez `BrevoMailSender` albo `MailTrapMailSender`
- wybór providera przez konfigurację oraz Dependency Injection
- reguły biznesowe dla wysyłanych wiadomości:
  - dodanie prefiksu `[Q]`, gdy temat kończy się znakiem `?`
  - otoczenie nazwiska studenta w treści tagami `[student.name]` oraz `[/student.surname]`
- przechowywanie zarejestrowanych aplikacji i logów wysyłki w pamięci aplikacji
- izolacja logów, dzięki której każda aplikacja widzi tylko własne wpisy
- dokumentacja API w Swagger UI
- wygenerowany klient TypeScript i JavaScript oraz prosta strona demonstracyjna w folderze `WebClient`

## Architektura

Rozwiązanie jest podzielone na warstwy zgodnie z podejściem inspirowanym Clean Architecture:

- `MailSender.Api` - endpointy HTTP, autentykacja i konfiguracja aplikacji
- `MailSender.Application` - DTO, interfejsy i serwisy aplikacyjne
- `MailSender.Domain` - encje domenowe
- `MailSender.Infrastructure` - repozytoria, serwis JWT i implementacje providerów pocztowych
- `WebClient` - klient wygenerowany na podstawie OpenAPI i prosta strona HTML

## Konfiguracja

Dane wrażliwe nie są przechowywane w kodzie ani w plikach konfiguracyjnych projektu. Należy ustawić je lokalnie przez .NET user-secrets albo zmienne środowiskowe.

Przykładowa konfiguracja lokalna:

```powershell
dotnet user-secrets set "Jwt:Secret" "twoj-lokalny-sekret-jwt" --project MailSender.Api\MailSender.Api.csproj
dotnet user-secrets set "Brevo:ApiKey" "twoj-klucz-api-brevo" --project MailSender.Api\MailSender.Api.csproj
dotnet user-secrets set "Brevo:SenderEmail" "zweryfikowany-nadawca@example.com" --project MailSender.Api\MailSender.Api.csproj
dotnet user-secrets set "Mailtrap:ApiKey" "twoj-klucz-api-mailtrap" --project MailSender.Api\MailSender.Api.csproj
dotnet user-secrets set "Mailtrap:SenderEmail" "hello@demomailtrap.co" --project MailSender.Api\MailSender.Api.csproj
```

Aktywnego providera pocztowego wybiera się w pliku `MailSender.Api/appsettings.json`:

```json
{
  "MailProvider": "Brevo"
}
```

albo:

```json
{
  "MailProvider": "Mailtrap"
}
```

## Uruchomienie

```powershell
dotnet run --project MailSender.Api
```

Po uruchomieniu Swagger UI jest dostępny pod adresem:

```text
http://localhost:5215/swagger
```

## Główny przepływ API

### Rejestracja aplikacji klienckiej

`POST /client-app/register`

```json
{
  "appId": "demo-app",
  "appName": "Demo App",
  "pass": "q##waQ85"
}
```

Przykładowa poprawna odpowiedź:

```json
{
  "appId": "demo-app",
  "appName": "Demo App",
  "key": "jwt-token"
}
```

Próba ponownej rejestracji aplikacji z tym samym `appId` albo `appName` zwraca `409 Conflict`.

### Wysyłanie wiadomości

`POST /mail/send`

Endpoint wymaga tokenu JWT przekazanego jako Bearer token.

```json
{
  "to": "odbiorca@example.com",
  "subject": "Czy działa?",
  "body": "Test Skutecki wiadomości"
}
```

Przykładowa odpowiedź:

```json
{
  "appId": "demo-app",
  "appName": "Demo App",
  "status": "queued",
  "email": {
    "to": "odbiorca@example.com",
    "subject": "[Q] Czy działa?",
    "body": "Test [student.name]Skutecki[/student.surname] wiadomości"
  }
}
```

### Logi wysyłek

- `GET /mail-log` - zwraca logi tylko dla aktualnie zalogowanej aplikacji
- `GET /mail-log/{id}` - zwraca konkretny log tylko wtedy, gdy należy on do aktualnie zalogowanej aplikacji

## WebClient

Folder `WebClient` zawiera:

- bibliotekę TypeScript wygenerowaną na podstawie specyfikacji OpenAPI
- wynikową bibliotekę JavaScript
- prostą stronę HTML do wysyłania wiadomości przez API
