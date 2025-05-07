using System.Text.Json;

namespace TravelHelper.Api.Services;

public class CountryClient : ICountryClient
{
    private readonly HttpClient _http;
    public CountryClient(HttpClient http) => _http = http;

    public async Task<CountryInfo?> GetAsync(string isoCode)
    {
        var url = $"https://restcountries.com/v3.1/alpha/{isoCode}";
        var json = await _http.GetStringAsync(url);

        using var doc = JsonDocument.Parse(json);
        var root = doc.RootElement[0];

        string name = root.GetProperty("name").GetProperty("common").GetString()!;
        string flag = root.GetProperty("flags").GetProperty("png").GetString()!;

        var firstCurProp = root.GetProperty("currencies").EnumerateObject().First();
        string currencyCode = firstCurProp.Name;

        return new CountryInfo(name, flag, currencyCode);
    }
}