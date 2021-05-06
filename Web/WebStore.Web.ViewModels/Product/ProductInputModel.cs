﻿namespace WebStore.Web.ViewModels.Product
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;


    public class ProductInputModel
    {
        [MinLength(10)]
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        [Range(typeof(decimal), "0", "79228162514264337593543950335", ErrorMessage ="Price must be positive")]
        public decimal Price { get; set; }

        [MinLength(10)]
        [Required]
        [Display(Name = "Short Desription")]
        public string ShortDescription { get; set; }

        [Url]
        [Required]
        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; }

        [Display(Name = "Categories")]
        public string CategoryId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> Categories { get; set; }
    }
}