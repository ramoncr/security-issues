using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Noteing.API.Data;
using Noteing.API.Services;

namespace Noteing.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FilesController : ControllerBase
    {
        private readonly ImageCropService _imageCropService;
        private readonly StorageService _storageService;
        private readonly ApplicationDbContext _dbContext;

        public FilesController(ImageCropService imageCropService, StorageService storageService, ApplicationDbContext dbContext)
        {
            _imageCropService = imageCropService;
            _storageService = storageService;
            _dbContext = dbContext;
        }

        [HttpPost("image")]
        public async Task<IActionResult> UploadImages(List<IFormFile> files)
        {
            foreach (var file in files)
            {
                var filePath = Path.Combine("/uploads/images/", Path.GetRandomFileName());

                using (var stream = System.IO.File.Create(filePath))
                {
                    await file.CopyToAsync(stream);
                }

                var croppedImages = await _imageCropService.CropImage(filePath, file.FileName);
                await _storageService.StorageCroppedImges(croppedImages);
            }

            return Ok();
        }

        [HttpGet("csv")]
        public IActionResult ExportAsCSV()
        {
            var notes = _dbContext.Notes.ToList();

            var stringBuilder = new StringBuilder();

            // Add header
            stringBuilder.AppendLine(string.Format("{0},{1}", "Id", "Name"));

            // Add notes
            foreach (var note in notes)
            {
                var noteLine = string.Format("{0},{1}", note.Id, note.Name);
                stringBuilder.AppendLine(noteLine);
            }

            // Convert to exportable format
            var contents = Encoding.UTF8.GetBytes(stringBuilder.ToString());

            return File(contents, "text/csv", "notes.csv");
        }
    }
}
