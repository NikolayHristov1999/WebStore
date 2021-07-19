namespace WebStore.Web.ViewModels.Administration.Files
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Microsoft.AspNetCore.Http;

    public class AddFilesInputModel
    {
        [Required]
        public IEnumerable<IFormFile> AddFiles { get; set; }

        public string Path { get; set; }
    }
}
