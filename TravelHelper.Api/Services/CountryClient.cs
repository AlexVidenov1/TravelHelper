using System.Text.Json;
namespace TravelHelper.Api.Services;
public class CountryClient : ICountryClient
{
    private readonly HttpClient _http;
    public CountryClient(HttpClient http) { _http = http; }
    public async Task<dynamic?> GetAsync(string name)
    {
        var url = $"https://restcountries.com/v3.1/name/{name}";
        var json = await _http.GetStringAsync(url);
        return JsonSerializer.Deserialize<dynamic>(json);
    }
}