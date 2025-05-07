namespace TravelHelper.Api.Services;
public interface ICountryClient
{
    Task<CountryInfo?> GetAsync(string isoCode);
}

public record CountryInfo(string Name, string FlagPng, string Currency);