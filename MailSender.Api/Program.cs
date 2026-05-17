using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MailSender.Application.Interfaces;
using MailSender.Application.Services;
using MailSender.Infrastructure.Providers;
using MailSender.Infrastructure.Repositories;
using MailSender.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Rejestracja kontrolerow API
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("WebClient", policy =>
    {
        policy
            .WithOrigins(
                "http://localhost:8080",
                "http://127.0.0.1:8080",
                "http://localhost:8000",
                "http://127.0.0.1:8000"
            )
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

// Wybor providera maili na podstawie konfiguracji
var mailProvider = builder.Configuration["MailProvider"];

if (mailProvider == "Brevo")
{
    builder.Services.AddHttpClient<IMailSenderProvider, BrevoMailSender>();
}
else if (mailProvider == "Mailtrap")
{
    builder.Services.AddHttpClient<IMailSenderProvider, MailTrapMailSender>();
}
else if (mailProvider == "Console")
{
    builder.Services.AddScoped<IMailSenderProvider, ConsoleMailSenderProvider>();
}
else
{
    builder.Services.AddScoped<IMailSenderProvider, FakeMailSenderProvider>();
}

// Konfiguracja Swaggera i tokena Bearer
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Wpisz token JWT w formacie: Bearer {token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var jwtSecret = builder.Configuration["Jwt:Secret"];

if (string.IsNullOrWhiteSpace(jwtSecret))
{
    throw new InvalidOperationException("Missing configuration value: Jwt:Secret");
}

builder.Services.AddSingleton<ITokenService>(
    new JwtTokenService(jwtSecret)
);
builder.Services.AddSingleton<IClientAppRepository, InMemoryClientAppRepository>();
builder.Services.AddSingleton<IMailLogRepository, InMemoryMailLogRepository>();
builder.Services.AddScoped<ClientAppService>();
builder.Services.AddScoped<MailService>();
builder.Services.AddScoped<MailLogService>();
var jwtKey = Encoding.UTF8.GetBytes(jwtSecret);

// Konfiguracja uwierzytelniania JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(jwtKey)
        };
    });

// Dodanie autoryzacji
builder.Services.AddAuthorization();

var app = builder.Build();

// Swagger dostepny w trybie developerskim
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("WebClient");

// Przekierowanie na HTTPS
app.UseHttpsRedirection();

// Uzycie uwierzytelniania i autoryzacji
app.UseAuthentication();
app.UseAuthorization();

// Mapowanie kontrolerow
app.MapControllers();

app.Run();
