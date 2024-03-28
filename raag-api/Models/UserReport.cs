using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace raag_api.Models
{
    public class UserReport
    {
        [Key]
        public int Id { get; set; }
        public DateTime Timestamp { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Report { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; }
        public bool? IsImagePermission { get; set; }
        public string? ImageName { get; set; }

        [NotMapped]
        public string ImageSrc { get; set; }
    }
}
