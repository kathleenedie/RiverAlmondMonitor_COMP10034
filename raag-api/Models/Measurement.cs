using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace raag_api.Models
{
    public class Measurement
    {
        [Key]
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public double? Value { get; set; }

        [ForeignKey("TimeSeries")]
        public int TimeSeriesId { get; set; }
        public TimeSeries? TimeSeries { get; set; }
    }
}
