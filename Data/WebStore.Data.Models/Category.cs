namespace WebStore.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    using WebStore.Data.Common.Models;

    public class Category : BaseDeletableModel<int>
    {
        public Category()
        {
            this.Products = new HashSet<CategoryProduct>();
            this.ChildCategories = new HashSet<Category>();
        }

        [Required]
        [MinLength(3)]
        public string Name { get; set; }

        [Required]
        [MinLength(10)]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public int? ParentCategoryId { get; set; }

        public Category ParentCategory { get; set; }

        public ICollection<CategoryProduct> Products { get; set; }

        public ICollection<Category> ChildCategories { get; set; }
    }
}
