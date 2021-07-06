namespace WebStore.Services.Data
{
    using Microsoft.AspNetCore.Http;
    using System.Threading.Tasks;

    public interface IImageProcessing
    {
        Task<string> UploadImageAsync(IFormFile image, string path, string userId = null);
    }
}
