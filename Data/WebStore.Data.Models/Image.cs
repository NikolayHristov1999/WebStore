namespace WebStore.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using WebStore.Data.Common.Models;

    public class Image : BaseModel<string>
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Required]
        public string FullPath { get; set; }

        public string Extension { get; set; }

        public string AltTagName { get; set; }

        public string AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }
    }
}
