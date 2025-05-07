namespace TravelHelper.Api.Services;

public record WeatherInfo
(
    decimal Temp,
    string Description,
    string Icon,
    string CountryIso
);