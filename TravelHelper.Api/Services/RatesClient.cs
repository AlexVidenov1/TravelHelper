using System.Text.Json;

namespace TravelHelper.Api.Services
{
    public class RatesClient : IRatesClient
    {
        private readonly HttpClient _http;
        public RatesClient(HttpClient http) => _http = http;

        public async Task<decimal?> ConvertAsync(string from, string to)
        {
            var url = $"https://open.er-api.com/v6/latest/{from}";
            var json = await _http.GetStringAsync(url);
            using var doc = JsonDocument.Parse(json);

            if (doc.RootElement.GetProperty("result").GetString() != "success")
                return null;

            var rate = doc.RootElement
                          .GetProperty("rates")
                          .GetProperty(to)
                          .GetDecimal();
            return rate;
        }
    }
}