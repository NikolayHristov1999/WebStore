namespace WebStore.Services.Data.Contracts
{
    using Microsoft.AspNetCore.Http;

    using System.Threading.Tasks;

    public interface IImageProcessingService
    {
        Task<string> UploadImageAsync(IFormFile image, string path, string userId = null);
    }
}
