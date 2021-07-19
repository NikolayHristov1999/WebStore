namespace WebStore.Web.ViewModels.Administration.Files
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class FileViewModel
    {
        public string Name { get; set; }

        public string UrlPath { get; set; }

        public long Size { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsDirectory { get; set; }
    }
}
