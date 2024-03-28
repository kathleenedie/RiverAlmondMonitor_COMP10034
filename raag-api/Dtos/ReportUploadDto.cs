using raag_api.Models;

namespace raag_api.Dtos
{
    public class ReportUploadDto
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Report { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? Email { get; set; }
        public bool? IsImagePermission { get; set; }
        public string? ImageName { get; set; }
        public IFormFile ImageFile { get; set; }

    }
}
