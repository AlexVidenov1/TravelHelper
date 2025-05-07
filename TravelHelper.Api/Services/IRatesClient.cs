namespace TravelHelper.Api.Services;
public interface IRatesClient
{
    Task<dynamic?> GetAsync(string baseCurrency);
}