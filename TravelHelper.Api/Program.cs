using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TravelHelper.Api.Data;
using TravelHelper.Api.Services;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);
var cfg = builder.Configuration;

// ---------------- DB ----------------
builder.Services.AddDbContext<TravelContext>(
o => o.UseSqlite(cfg.GetConnectionString("Default")));

// ------------- JWT ------------------
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = cfg["Jwt:Issuer"],
        ValidateAudience = false,
        IssuerSigningKey =
    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(cfg["Jwt:Key"])),
        ValidateIssuerSigningKey = true
    };
});

// --------- Swagger --------------
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // … вече генерираните настройки
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT in header. Format: Bearer {token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
{
    {
        new OpenApiSecurityScheme
        {
            Reference = new OpenApiReference
            { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
        },
        Array.Empty<string>()
    }
});
});

// -------- HTTP clients ----------
builder.Services.AddHttpClient<IWeatherClient, WeatherClient>();
builder.Services.AddHttpClient<ICountryClient, CountryClient>();
builder.Services.AddHttpClient<IRatesClient, RatesClient>();
builder.Services.AddScoped<IDestinationAggregator, DestinationAggregator>();

builder.Services.AddControllers();
var app = builder.Build();

// ---------- pipeline -------------
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseAuthentication();
app.UseAuthorization();
app.UseCors(p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.MapControllers();
app.Run();