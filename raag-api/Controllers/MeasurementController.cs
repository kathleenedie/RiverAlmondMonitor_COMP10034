using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using raag_api.Data;
using raag_api.Models;

namespace raag_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MeasurementController : ControllerBase
    {
        private readonly HttpClient _client;
        private readonly DataContext _context;

        public MeasurementController(HttpClient client, DataContext context)
        {
            _client = client;
            _context = context;
        }

        [HttpGet("SepaMeasurements")]
        public async Task<IActionResult> GetFlowMeasurementsAsync()
        {
            var url =
                "https://timeseries.sepa.org.uk/KiWIS/KiWIS?service=kisters&type=queryServices&datasource=0&request=getTimeseriesValues&ts_id=61873010,61865010,61896010,54393010,54250010,54283010,54405010&returnfields=Timestamp,Value&format=json";
            var response = await _client.GetAsync(url);
            var responseObject = await response.Content.ReadAsStringAsync();

            JArray jArray = JArray.Parse(responseObject);

            var result = new List<Measurement>();

            for (var i = 0; i < jArray.Count; i++)
            {
                result.Add(new Measurement
                {
                    TimeSeriesId = (int)jArray[i]["ts_id"],
                    Timestamp = (DateTime)jArray[i]["data"][0][0],
                    Value = (double?)jArray[i]["data"][0][1]
                });
            }

            await _context.Measurements.AddRangeAsync(result);
            await _context.SaveChangesAsync();

            return Ok(response);
        }
    }
}
