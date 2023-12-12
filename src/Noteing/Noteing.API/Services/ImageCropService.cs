using Noteing.API.Models;

namespace Noteing.API.Services
{
    public class ImageCropService
    {
        private readonly string[] Sizes = new string[3] { "100+100", "200+200", "500+500" };

        public async Task<List<CroppedImage>> CropImage(string filePath, string fileName)
        {
            var results = new List<CroppedImage>();

            foreach (var size in Sizes)
            {
                var croppedFileName = size + fileName;
                var path = Path.GetTempPath() + croppedFileName;

                System.Diagnostics.Process process = new System.Diagnostics.Process();
                System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
                startInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                startInfo.FileName = "cmd.exe";
                startInfo.Arguments = $"convert -crop {size} {filePath} {Path.GetTempPath()}{size}{fileName}";
                process.StartInfo = startInfo;
                process.Start();

                results.Add(new CroppedImage
                {
                    Name = croppedFileName,
                    Path = path,
                    Size = size,
                });

            }

            return results;
        }
    }
}
