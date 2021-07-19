namespace WebStore.Web.ViewModels.Administration.Files
{
    using Microsoft.AspNetCore.Http;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class FilesTableViewModel
    {
        public IEnumerable<FileViewModel> Files { get; set; }

        public long TotalSize { get; set; }

        [RegularExpression(@"^([a-zA-Z0-9][^*/><?\|:]*)$", ErrorMessage = "Use symbols and digits only")]
        [MinLength(1)]
        [Required]
        public string NewFolderName { get; set; }

        [Required]
        public IEnumerable<IFormFile> AddFiles { get; set; }

        public string Path { get; set; }
    }
}
