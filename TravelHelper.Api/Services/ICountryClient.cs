namespace TravelHelper.Api.Services;
public interface ICountryClient
{
    Task<dynamic?> GetAsync(string countryName);
}