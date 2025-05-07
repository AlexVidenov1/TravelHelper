namespace TravelHelper.Api.Services
{
public interface IWeatherClient
{
Task<WeatherInfo?> GetAsync(string city);
}
}