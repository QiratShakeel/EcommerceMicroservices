using Ecommerce.Catalog.Application.Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Catalog.Infrastructure.Services.FileStorage
{
    public class LocalFileService : IFileService
    {
        private readonly IWebHostEnvironment _env;

        public LocalFileService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> UploadAsync(IFormFile file, string folderName)
        {
            var uploadsPath = Path.Combine(_env.WebRootPath, "uploads", folderName);

            if (!Directory.Exists(uploadsPath))
                Directory.CreateDirectory(uploadsPath);

            var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsPath, fileName);

            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);

            return $"/uploads/{folderName}/{fileName}";
        }
        public async Task DeleteAsync(string fileUrl)
        {
            if (string.IsNullOrWhiteSpace(fileUrl))
                return;

            var relativePath = fileUrl.TrimStart('/')
                                      .Replace('/', Path.DirectorySeparatorChar);

            var fullPath = Path.Combine(_env.WebRootPath, relativePath);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }

            await Task.CompletedTask;
        }
    }
}
