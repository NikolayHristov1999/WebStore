namespace WebStore.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Http;
    using WebStore.Web.ViewModels.Administration.Files;

    public interface IFilesService
    {
        IEnumerable<FileViewModel> GetFilesInFolder(string fullPath, string urlPath);

        Task<bool> AddFileAsync(IFormFile file, string path, string userId);
    }
}
