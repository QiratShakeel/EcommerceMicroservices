
using Microsoft.AspNetCore.Http;

namespace Ecommerce.Catalog.Application.Interfaces{
    public interface IFileService
    {
        Task<string> UploadAsync(IFormFile file, string folderName);
        Task DeleteAsync(string fileUrl);
    }
}

