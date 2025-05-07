using System.Text.Json;

namespace TravelHelper.Api.Services;
public class WeatherClient : IWeatherClient
{
    private readonly HttpClient _http;
    private readonly IConfiguration _cfg;
    public WeatherClient(HttpClient http, IConfiguration cfg)
    {
        _http = http; _cfg = cfg;
    }
    public async Task<dynamic?> GetAsync(string city)
    {
        var key = _cfg["OpenWeather:Key"];
        Console.WriteLine($"OWM key = {key}");
        var url = $"https://api.openweathermap.org/data/2.5/weather" +
                  $"?q={city}&appid={key}&units=metric";

        var resp = await _http.GetAsync(url);
        if (!resp.IsSuccessStatusCode) return null;   // вместо exception

        var json = await resp.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<dynamic>(json);
    }
}