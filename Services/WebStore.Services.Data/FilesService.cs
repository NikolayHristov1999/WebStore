namespace WebStore.Services.Data
{
    using Microsoft.AspNetCore.Http;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using WebStore.Services.Data.Contracts;
    using WebStore.Web.ViewModels.Administration.Files;

    public class FilesService : IFilesService
    {
        public async Task<bool> AddFileAsync(IFormFile file, string path)
        {
            if (file.Length > 0)
            {
                using (var stream = File.Create(path + "/" + file.FileName))
                {
                    await file.CopyToAsync(stream);
                }

                return true;
            }

            return false;
        }

        public IEnumerable<FileViewModel> GetFilesInFolder(string fullPath, string urlPath)
        {
            var filePaths = Directory.GetFiles(fullPath);
            var folderPaths = Directory.GetDirectories(fullPath);
            var files = new List<FileViewModel>();

            foreach (string filePath in folderPaths)
            {
                var directory = new DirectoryInfo(filePath);

                files.Add(new FileViewModel
                {
                    Name = directory.Name,
                    CreatedOn = directory.CreationTimeUtc,
                    UrlPath = Path.Combine(urlPath, directory.Name),
                    Size = this.DirSize(directory),
                    IsDirectory = true,
                });
            }

            foreach (string filePath in filePaths)
            {
                var file = new FileInfo(filePath);

                files.Add(new FileViewModel
                {
                    Name = file.Name,
                    CreatedOn = file.CreationTime,
                    UrlPath = Path.Combine(urlPath, file.Name),
                    Size = file.Length,
                });
            }

            return files;
        }

        private long DirSize(DirectoryInfo d)
        {
            long size = 0;
            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis)
            {
                size += fi.Length;
            }
            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                size += this.DirSize(di);
            }
            return size;
        }
    }
}
