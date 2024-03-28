using raag_api.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace raag_api.Dtos;

public class LabelledMeasurementDto
{
    public int Id { get; set; }
    public DateTime Timestamp { get; set; }
    public int TimeSeriesId { get; set; }
    public string Description { get; set; }
    public double? Value { get; set; }
    public string StationName { get; set; }

}