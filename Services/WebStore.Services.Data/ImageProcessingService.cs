namespace WebStore.Services.Data
{
    using System;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;
    using WebStore.Data.Common.Repositories;
    using WebStore.Data.Models;
    using WebStore.Services.Data.Contracts;

    public class ImageProcessingService : IImageProcessingService
    {
        private const string DirectoryImagePath = "wwwroot/images/";

        private readonly string[] allowedExtensions = new[] { "jpg", "png", "gif" };
        private readonly IRepository<Image> imageRepository;

        public ImageProcessingService(IRepository<Image> imageRepository)
        {
            this.imageRepository = imageRepository;
        }
        /// <summary>
        ///     Upload image in the file system
        /// </summary>
        /// <param name="path">/wwwroot/images/{path}...
        ///     Example - path = products/productId
        /// </param>
        public async Task<string> UploadImageAsync(IFormFile image, string path, string userId = null)
        {
            var imagePath = DirectoryImagePath + path.Trim().TrimEnd('/');
            var extension = Path.GetExtension(image.FileName).TrimStart('.');

            if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
            {
                throw new Exception($"Invalid image extension {extension}");
            }

            if (!Directory.Exists(imagePath))
            {
                Directory.CreateDirectory(imagePath);
            }

            var dbImage = new Image
            {
                AddedByUserId = userId,
                FullPath = Path.GetFullPath(imagePath),
                Extension = extension,
            };

            var physicalPath = $"{imagePath}/{dbImage.Id}.{extension}";
            using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
            await image.CopyToAsync(fileStream);

            await this.imageRepository.AddAsync(dbImage);
            await this.imageRepository.SaveChangesAsync();

            return Path.GetFullPath(imagePath);
        }
    }
}
