namespace WebStore.Web.ViewModels.Administration.Files
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    public class CreateFolderInputModel
    {
        public string Path { get; set; }

        [RegularExpression(@"^([a-zA-Z0-9][^*/><?\|:]*)$")]
        [MinLength(1)]
        [Required]
        public string NewFolderName { get; set; }
    }
}
