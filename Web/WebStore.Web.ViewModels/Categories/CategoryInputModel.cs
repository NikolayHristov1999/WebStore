namespace WebStore.Web.ViewModels.Categories
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    public class CategoryInputModel
    {
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MinLength(10)]
        public string Description { get; set; }

        [Required]
        [Url]
        public string ImageUrl { get; set; }

        public string CategoryId { get; set; }
        public IEnumerable<KeyValuePair<string, string>> Categories { get; set; }
    }
}
