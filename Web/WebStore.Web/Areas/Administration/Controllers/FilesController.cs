namespace WebStore.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.FileProviders;
    using WebStore.Common;
    using WebStore.Services.Data.Contracts;
    using WebStore.Web.Infrastructure.Extensions;
    using WebStore.Web.ViewModels.Administration.Files;

    public class FilesController : AdministrationController
    {
        private const string UsersFilePath = "\\UsersFiles";
        private readonly IFilesService filesService;

        public FilesController(
            IFilesService filesService)
        {
            this.filesService = filesService;
        }

        public IActionResult Index(string requestedPath)
        {
            var requestPath = requestedPath == null ? string.Empty : requestedPath.Replace("%2F", "/");
            var userId = this.User.GetId();
            var path = Path.Combine(GlobalConstants.UsersFileRootFolder, userId);
            var fullPath = Path.GetFullPath(Path.Combine(path, requestedPath ?? string.Empty));
            var urlPath = Path.Combine(UsersFilePath, userId, requestedPath ?? string.Empty);

            if (!Directory.Exists(fullPath))
            {
                // This path is a directory
                return this.NotFound();
            }

            var files = this.filesService.GetFilesInFolder(fullPath, urlPath);

            var model = new FilesTableViewModel
            {
                Files = files,
                TotalSize = files.Sum(x => x.Size),
                Path = requestedPath ?? string.Empty,
            };

            return this.View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult CreateFolder(CreateFolderInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(this.Index));
            }

            var userId = this.User.GetId();
            var folderPath = Path.Combine(
                Path.Combine(GlobalConstants.UsersFileRootFolder, userId),
                model.Path ?? string.Empty);
            var fullPath = Path.GetFullPath(Path.Combine(folderPath, model.NewFolderName));

            if (!Directory.Exists(fullPath))
            {
                // This path is a directory
                Directory.CreateDirectory(fullPath);
            }

            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult AddFiles(AddFilesInputModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(this.Index));
            }

            var userId = this.User.GetId();
            var folderPath = Path.Combine(
                Path.Combine(GlobalConstants.UsersFileRootFolder, userId),
                model.Path ?? string.Empty);

            if (!Directory.Exists(folderPath))
            {
                // This path is a directory
                Directory.CreateDirectory(folderPath);
            }

            foreach (var file in model.AddFiles)
            {
                this.filesService.AddFileAsync(file, Path.GetFullPath(folderPath));
            }

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
