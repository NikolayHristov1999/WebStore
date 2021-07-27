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
            this.Items = new HashSet<Item>();
        }

        [MinLength(3)]
        [Required]
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string ShortDescription { get; set; }

        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        public int AvailableQuantity { get; set; }

        public string AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }

        public long Views { get; set; }

        public string MadeIn { get; set; }

        public string StoredInCountry { get; set; }

        public ICollection<CategoryProduct> Categories { get; set; }

        public ICollection<Item> Items { get; set; }

    }
}
