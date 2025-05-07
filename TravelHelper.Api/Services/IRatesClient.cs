namespace TravelHelper.Api.Services;
public interface IRatesClient
{
Task<decimal?> ConvertAsync(string from, string to);
}