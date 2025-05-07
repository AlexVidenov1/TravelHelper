namespace TravelHelper.Api.Services;
public class DestinationAggregator : IDestinationAggregator
{
    private readonly IWeatherClient _weather;
    private readonly ICountryClient _country;
    private readonly IRatesClient _rates;
    public DestinationAggregator(IWeatherClient w, ICountryClient c, IRatesClient r)
    { _weather = w; _country = c; _rates = r; }

    public async Task<object> AggregateAsync(string city)
    {
        var weather = await _weather.GetAsync(city);
        if (weather == null) return new { error = "city not found" };
        string countryName = weather?.sys?.country ?? "";
        var countryInfo = await _country.GetAsync(countryName);
        var rates = await _rates.GetAsync("EUR");

        return new
        {
            city,
            weather,
            country = countryInfo,
            rates
        };
    }
}