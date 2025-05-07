using System.Text.Json;
using TravelHelper.Api.Services;

namespace TravelHelper.Api.Services;

public class WeatherClient : IWeatherClient
{
    private readonly HttpClient _http;
    private readonly IConfiguration _cfg;
    public WeatherClient(HttpClient http, IConfiguration cfg)
    {
        _http = http; _cfg = cfg;
    }
    public async Task<WeatherInfo?> GetAsync(string city)
{
    var key = _cfg["OpenWeather:Key"];
    var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={key}&units=metric";
    var resp = await _http.GetAsync(url);
    if (!resp.IsSuccessStatusCode) return null;

    var json = await resp.Content.ReadAsStringAsync();
    using var doc = JsonDocument.Parse(json);
    var root = doc.RootElement;

    decimal temp = root.GetProperty("main").GetProperty("temp").GetDecimal();
    string desc = root.GetProperty("weather")[0].GetProperty("description").GetString()!;
    string icon = root.GetProperty("weather")[0].GetProperty("icon").GetString()!;
    string country = root.GetProperty("sys").GetProperty("country").GetString()!;

    return new WeatherInfo(temp, desc, icon, country);
}
}