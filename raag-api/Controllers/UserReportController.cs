using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using raag_api.Data;
using raag_api.Dtos;
using raag_api.Models;

namespace raag_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserReportController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public UserReportController(HttpClient httpClient, DataContext dataContext, IWebHostEnvironment webHostEnvironment)
        {
            _dataContext = dataContext;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet("UserReports")]
        public async Task<ActionResult<IEnumerable<UserReport>>> GetUserReports()
        {
            return await _dataContext.UserReports
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

        [HttpPost("UserReport")]
        public async Task<ActionResult<UserReport>> CreateUserReport([FromForm]ReportUploadDto userReportUpload)
        {
            userReportUpload.ImageName = await SaveImage(userReportUpload.ImageFile);
            var userReport = new UserReport
            {
                Timestamp = DateTime.Now,
                Latitude = userReportUpload.Latitude,
                Longitude = userReportUpload.Longitude,
                Report = userReportUpload.Report,
                FirstName = userReportUpload.FirstName,
                LastName = userReportUpload.LastName,
                Email = userReportUpload.Email,
                IsImagePermission = userReportUpload.IsImagePermission,
                ImageName = userReportUpload.ImageName
            };

            _dataContext.UserReports.Add(userReport);
            await _dataContext.SaveChangesAsync();

            return new ObjectResult(userReport){StatusCode = StatusCodes.Status201Created};
        }

        [NonAction]
        public async Task<string> SaveImage(IFormFile file)
        {
            string imageName =
                new String(Path.GetFileNameWithoutExtension(file.FileName).Take(10).ToArray()).Replace(" ", "-");
            imageName = imageName + DateTime.Now.ToString("yymmssfff")+ Path.GetExtension(file.FileName);

            var imagePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return imageName;
        }
    }
}
