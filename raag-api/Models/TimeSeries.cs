using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace raag_api.Models
{
    public class TimeSeries
    {
        public int TimeSeriesid { get; set; }
        public string TimeSeriesName { get; set; }

        [ForeignKey("Station")]
        public int StationId { get; set; }
        public Station Station { get; set; }
        public int ParameterTypeId { get; set; }
        public string ParameterTypeName { get; set; }

        public ICollection<Measurement>? Measurements { get; set; }
    }
}
