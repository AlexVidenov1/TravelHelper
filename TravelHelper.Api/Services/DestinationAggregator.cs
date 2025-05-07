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
        var w = await _weather.GetAsync(city);
        if (w == null) return new { error = "City not found or weather API rejected." };

        var c = await _country.GetAsync(w.CountryIso);
        if (c == null) return new { error = "Country not found." };

        var rate = await _rates.ConvertAsync("EUR", c.Currency);
        if (rate == null)                       // only null if API really failed
            return new { error = "Rates API error." };

        return new
        {
            city,
            weather = new { w.Temp, w.Description, icon = $"https://openweathermap.org/img/wn/{w.Icon}@2x.png" },
            country = new { c.Name, c.FlagPng, currency = c.Currency },
            rate = new { eur_to_local = rate }
        };
    }
}