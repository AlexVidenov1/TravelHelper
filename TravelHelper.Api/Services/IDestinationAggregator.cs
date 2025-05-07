namespace TravelHelper.Api.Services;
public interface IDestinationAggregator
{
    Task<object> AggregateAsync(string city);
}