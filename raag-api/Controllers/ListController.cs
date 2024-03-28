using Microsoft.AspNetCore.Mvc;
using raag_api.Data;
using raag_api.Models;


namespace raag_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListController : Controller
    {
        private readonly HttpClient _client;
        private readonly DataContext _context;

        public ListController(HttpClient client, DataContext context)
        {
            _client = client;
            _context = context;
        }


        [HttpGet("TimeSeries")]
        public async Task<IActionResult> GetTimeSeries()
        {
            var url =
                "https://timeseries.sepa.org.uk/KiWIS/KiWIS?service=kisters&type=queryServices&datasource=0&request=getTimeseriesList&station_name=Almondell,Whitburn,Craigiehall&format=json";
            var response = await _client.GetFromJsonAsync<List<string[]>>(url);

            var timeSeries = new List<TimeSeries>();

            for (var i = 1; i < response.Count; i++)
            {
                var timeSeriesId = int.Parse(response[i][3]);
                if (_context.TimeSeries.All(a => a.TimeSeriesid != timeSeriesId))
                {
                    timeSeries.Add(new TimeSeries
                    {
                        TimeSeriesid = int.Parse(response[i][3]),
                        TimeSeriesName = response[i][4],
                        ParameterTypeId = int.Parse(response[i][5]),
                        ParameterTypeName = response[i][6],
                        StationId = int.Parse(response[i][2])
                    });
                }
            }

            await _context.TimeSeries.AddRangeAsync(timeSeries);
            await _context.SaveChangesAsync();

            return Ok(response);

        }
    }
}   
