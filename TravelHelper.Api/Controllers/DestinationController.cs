using Microsoft.AspNetCore.Mvc;
using TravelHelper.Api.Services;

namespace TravelHelper.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class DestinationController : ControllerBase
{
    private readonly IDestinationAggregator _agg;
    public DestinationController(IDestinationAggregator agg) { _agg = agg; }

    [HttpGet]
    public async Task<IActionResult> Get(string city)
    {
        var data = await _agg.AggregateAsync(city);
        return Ok(data);
    }
}