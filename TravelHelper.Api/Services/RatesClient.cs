using System.Text.Json;
namespace TravelHelper.Api.Services;
public class RatesClient : IRatesClient
{
    private readonly HttpClient _http;
    public RatesClient(HttpClient http) { _http = http; }
    public async Task<dynamic?> GetAsync(string baseCurrency)
    {
        var url = $"https://api.exchangerate.host/latest?base={baseCurrency}";
        var json = await _http.GetStringAsync(url);
        return JsonSerializer.Deserialize<dynamic>(json);
    }
}