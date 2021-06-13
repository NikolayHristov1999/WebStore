namespace WebStore.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using WebStore.Data.Common.Models;

    public class Product : BaseDeletableModel<int>
    {
        public Product()
        {
            this.Categories = new HashSet<CategoryProduct>();
        }

        [MinLength(3)]
        [Required]
        public string Name { get; set; }

        public decimal Price { get; set; }

        [Required]
        [MinLength(10)]
        public string ShortDescription { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public int AvailableQuantity { get; set; }

        public string AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }

        public ICollection<CategoryProduct> Categories { get; set; }

    }
}
