using System.ComponentModel.DataAnnotations;

namespace raag_api.Models
{
    public class Station
    {
        public int StationId { get; set; }
        public string StationName { get; set; }
        public int StationNo { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public ICollection<TimeSeries>? TimeSeries { get; set; }
    }
}
