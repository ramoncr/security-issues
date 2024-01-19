using System.IO.Compression;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using Noteing.API.Data;
using Noteing.API.Models;
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

        [HttpPost("image/{noteId}")]
        public async Task<IActionResult> UploadImages(List<IFormFile> files, [FromRoute] Guid noteId)
        {
            List<CroppedImage> croppedImages = null;
            foreach (var file in files)
            {
                var filePath = Path.Combine("/uploads/images/", Path.GetRandomFileName());

                using (var stream = System.IO.File.Create(filePath))
                {
                    await file.CopyToAsync(stream);
                }

                croppedImages = await _imageCropService.CropImage(filePath, file.FileName);
                var existingNote = _dbContext.Notes.FirstOrDefault(note => note.Id == noteId);
                if (existingNote is not null)
                {
                    existingNote.CroppedImages.AddRange(croppedImages);
                    _dbContext.Update(existingNote);
                    await _dbContext.SaveChangesAsync();
                }
            }

            return Ok(croppedImages);
        }

        [HttpPost("notes/{noteId}")]
        public IActionResult DownloadNoteWithAllFiles([FromRoute] Guid noteId, [FromBody] DownloadNote downloadNote)
        {
            var existingNote = _dbContext.Notes.FirstOrDefault(note => note.Id == noteId);
            if (existingNote is null)
                return NotFound();

            var temporaryFolder = Path.Combine(Path.GetTempPath(), downloadNote.ZipName) + Path.DirectorySeparatorChar;
            var temporaryZip = Path.Combine(Path.GetTempPath(), downloadNote.ZipName);
            Directory.CreateDirectory(temporaryFolder);

            foreach (var croppedImage in existingNote.CroppedImages)
            {
                System.IO.File.Copy(croppedImage.Path, Path.Combine(temporaryFolder, Path.GetFileName(croppedImage.Path)));
            }

            System.IO.File.WriteAllText(Path.Combine(temporaryFolder, "note.md"), existingNote.Content);

            ZipFile.CreateFromDirectory(temporaryFolder, temporaryZip);
            var fileStream = System.IO.File.OpenRead(temporaryZip);
            return File(fileStream, "application/zip");
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
