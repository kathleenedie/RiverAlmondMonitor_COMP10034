using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using raag_api.Data;
using raag_api.Dtos;
using raag_api.Models;


namespace raag_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly HttpClient _httpClient;

        public DashboardController(HttpClient httpClient, DataContext dataContext)
        {
            _httpClient = httpClient;
            _dataContext = dataContext;
        }

        [HttpGet("RainData")]
        public async Task<ActionResult<IEnumerable<Measurement>>> GetRainData()
        {
            return await _dataContext.Measurements
                .Where(m => m.TimeSeriesId == 54393010).ToListAsync();
        }

        [HttpGet("LevelData")]
        public async Task<ActionResult<IEnumerable<Measurement>>> GetLevelData()
        {
            return await _dataContext.Measurements
                .Where(m => m.TimeSeriesId == 54250010 || m.TimeSeriesId == 54283010 || m.TimeSeriesId == 54405010)
                .OrderByDescending(m => m.Timestamp)
                .Take(20)
                .ToListAsync();
        }

        [HttpGet("FlowData")]
        public async Task<ActionResult<IEnumerable<Measurement>>> GetFlowData()
        {
            return await _dataContext.Measurements
                .Where(m => m.TimeSeriesId == 61865010 || m.TimeSeriesId == 61873010 || m.TimeSeriesId == 61896010)
                .OrderByDescending(m => m.Timestamp)
                .Take(20)
                .ToListAsync();
        }

        [HttpPost("UserQuery")]
        public async Task<ActionResult<IEnumerable<LabelledMeasurementDto>>> GetUserQueryData([FromForm] DateTime startDate,
            [FromForm] DateTime endDate, [FromForm] string parameter)
        {
            var timeSeriesId = GetTimeSeriesIds(parameter);
            var results = new List<LabelledMeasurementDto>();

            var measurements = await _dataContext.Measurements
                .OrderByDescending(m => m.Timestamp)
                .Where(m => m.Timestamp >= startDate && m.Timestamp <= endDate && timeSeriesId.Contains(m.TimeSeriesId))
                .ToListAsync();

            measurements.ForEach(
                m => results.Add(GetStationNames(m, parameter)));

            return results;
        }

        [HttpGet("ConditionData")]
        public async Task<ActionResult<IEnumerable<Condition>>> GetConditionData()
        {
            return await _dataContext.Conditions
                .Where(c => c.Year == 2022)
                .OrderByDescending(c => c.Name)
                .ToListAsync();
        }

        private LabelledMeasurementDto GetStationNames(Measurement measurement, string parameter)
        {
            var craigiehallIds = new List<int>
            {
                54250010, 61865010
            };

            var almondellIds = new List<int>
            {
                54283010, 61873010
            };

            var whitburnIds = new List<int>
            {
                54393010, 54405010, 61896010
            };

            var labelledMeasurement = new LabelledMeasurementDto();

            if (craigiehallIds.Contains(measurement.TimeSeriesId))
            {
                return new LabelledMeasurementDto
                {
                    Id = measurement.Id,
                    StationName = "Craigiehall",
                    TimeSeriesId = measurement.TimeSeriesId,
                    Description = parameter,
                    Timestamp = measurement.Timestamp,
                    Value = measurement.Value,
                };
            }
            
            if (almondellIds.Contains(measurement.TimeSeriesId))
            {
                return new LabelledMeasurementDto
                {
                    Id = measurement.Id,
                    StationName = "Almondell",
                    TimeSeriesId = measurement.TimeSeriesId,
                    Description = parameter,
                    Timestamp = measurement.Timestamp,
                    Value = measurement.Value,
                };
            }

            if (whitburnIds.Contains(measurement.TimeSeriesId))
            {
                return new LabelledMeasurementDto
                {
                    Id = measurement.Id,
                    StationName = "Whitburn",
                    TimeSeriesId = measurement.TimeSeriesId,
                    Description = parameter,
                    Timestamp = measurement.Timestamp,
                    Value = measurement.Value,
                };
            }

            return labelledMeasurement;

        }

        private List<int> GetTimeSeriesIds(string parameter)
        {
            var timeSeriesIds = new List<int>();
            if (parameter == "rain")
            {
                timeSeriesIds.Add(54393010);
            }
            else if (parameter == "level")
            {
                timeSeriesIds.Add(54250010);
                timeSeriesIds.Add(54283010);
                timeSeriesIds.Add(54405010);
            }
            else if (parameter == "flow")
            {
                timeSeriesIds.Add(61865010);
                timeSeriesIds.Add(61873010);
                timeSeriesIds.Add(61896010);
            }

            return timeSeriesIds;

        }
    }
}
