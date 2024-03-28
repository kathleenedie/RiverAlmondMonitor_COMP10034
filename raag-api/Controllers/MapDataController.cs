using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using raag_api.Data;
using raag_api.Dtos;
using raag_api.Models;

namespace raag_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MapDataController : ControllerBase
    {
        private readonly DataContext _context;

        public MapDataController(DataContext context)
        {
            _context = context;

        }

        [HttpGet("Stations")]
        public async Task<ActionResult<List<Station>>> GetAllStations()
        {
            var stations = await _context.Stations.ToListAsync();

            return Ok(stations);
        }

        [HttpGet("MapMeasurements")]
        public async Task<ActionResult<IEnumerable<MapMeasurementDto>>> GetMapMeasurements()
        {
            var rain = "Precip";
            var flow = "Q";
            var level = "S";

            var types = new[] {rain, flow, level};
            

            var almondell = 36404;
            var craigiehall = 36396;
            var whitburn = 36432;

            var stations = new[] {almondell, craigiehall, whitburn};

            var measurements = new List<MapMeasurementDto>();

            var stationData = _context.Stations
                .ToList();

            foreach (var stationDatum in stationData)
            {
                var stationMeasurement = new MapMeasurementDto
                {
                    StationId = stationDatum.StationId,
                    Latitude = stationDatum.Latitude,
                    Longitude = stationDatum.Longitude,
                    Values = new List<ValueDto>()
                };

                var stationReadings = _context.Measurements.Include(m => m.TimeSeries)
                            .ThenInclude(t => t.Station)
                            .Where(m => m.TimeSeries.StationId == stationDatum.StationId)
                            .OrderByDescending(m => m.Timestamp)
                            .Take(10).ToList();

                        var latestRainReading = stationReadings.FirstOrDefault(m =>m.TimeSeries?.ParameterTypeName == rain);
                        if (latestRainReading != null)
                        {
                            stationMeasurement.Values.Add(createValueDto(latestRainReading));
                        }

                        var latestFlowReading = stationReadings.FirstOrDefault(m => m.TimeSeries?.ParameterTypeName == flow);
                        if (latestFlowReading != null)
                        {
                            stationMeasurement.Values.Add(createValueDto(latestFlowReading));
                        }

                        var latestLevelReading = stationReadings.FirstOrDefault(m => m.TimeSeries?.ParameterTypeName == level);
                        if (latestLevelReading != null)
                        {
                            stationMeasurement.Values.Add(createValueDto(latestLevelReading));
                        }
                    

                measurements.Add(stationMeasurement);
            }

            return Ok(measurements);
        }

        [HttpGet("UserReports")]
        public async Task<ActionResult<IEnumerable<UserReport>>> GetUserReports()
        {
            return await _context.UserReports
                .Select(u => new UserReport
                {
                    Id = u.Id,
                    Timestamp = u.Timestamp,
                    Latitude = u.Latitude,
                    Longitude = u.Longitude,
                    Report = u.Report,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    Email = u.Email,
                    IsImagePermission = u.IsImagePermission,
                    ImageName = u.ImageName,
                    ImageSrc = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase,
                        u.ImageName)
                }).ToListAsync();
        }

        private ValueDto createValueDto(Measurement reading)
        {
            var stationParameterName = "";

            if (reading.TimeSeries.ParameterTypeId == ApplicationConstants.RainParameter)
            {
                stationParameterName = "Rain";
            };
            if (reading.TimeSeries.ParameterTypeId == ApplicationConstants.FlowParameter)
            {
                stationParameterName = "Flow";
            };
            if (reading.TimeSeries.ParameterTypeId == ApplicationConstants.LevelParameter)
            {
                stationParameterName = "Level";
            };


            var readingResult = new ValueDto
            {
                Timestamp = reading.Timestamp,
                Value = reading.Value,
                StationParameterName = stationParameterName
            };
            return readingResult;
        }
    }
};
