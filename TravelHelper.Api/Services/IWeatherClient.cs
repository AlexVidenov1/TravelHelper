namespace TravelHelper.Api.Services;
public interface IWeatherClient
{
    Task<dynamic?> GetAsync(string city);
}