using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MailSender.Application.Interfaces;
using MailSender.Infrastructure.Providers;

var builder = WebApplication.CreateBuilder(args);

// Kontrolery API
builder.Services.AddControllers();

// Wybor providera maili na podstawie appsettings.json
var mailProvider = builder.Configuration["MailProvider"];
// Console.WriteLine($"Selected mail provider: {mailProvider}");

if (mailProvider == "Console")
{
    builder.Services.AddScoped<IMailSenderProvider, ConsoleMailSenderProvider>();
}
else
{
    builder.Services.AddScoped<IMailSenderProvider, FakeMailSenderProvider>();
}

// Swagger UI + obsluga tokena Bearer
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

// Klucz musi byc taki sam jak w JwtTokenService
var jwtKey = Encoding.UTF8.GetBytes("SuperSecretKeyForMailSenderProject123456!");

// Konfiguracja JWT
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

// HTTPS
app.UseHttpsRedirection();

// JWT
app.UseAuthentication();
app.UseAuthorization();

// Mapowanie kontrolerow
app.MapControllers();

app.Run();