namespace WebStore.Web.ViewModels.Administration.Products
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper.Configuration.Annotations;
    using WebStore.Services.Mapping;
    using WebStore.Data.Models;

    public class CreateProductViewModel : IMapFrom<Product>, IMapTo<Product>
    {
        [MinLength(10)]
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }

        [Range(typeof(decimal), "0", "79228162514264337593543950335", ErrorMessage = "Price must be positive")]
        public decimal Price { get; set; }

        [Display(Name = "Short Desription")]
        public string ShortDescription { get; set; }

        [Display(Name = "Main Desription")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Image Url")]
        public string ImageUrl { get; set; }

        [Display(Name = "Quantity")]
        public int AvailableQuantity { get; set; }

        [Display(Name = "First Category")]
        [Ignore]
        public string FirstCategory { get; set; }

        [Display(Name = "Second Category")]
        [Ignore]
        public string SecondCategory { get; set; }

        [Display(Name = "Third Category")]
        [Ignore]
        public string ThirdCategory { get; set; }

        [Display(Name = "Made in")]
        public string MadeIn { get; set; }

        [Display(Name = "Stored in country")]
        public string StoredInCountry { get; set; }

        [Ignore]
        public IEnumerable<KeyValuePair<string, string>> AllCategories { get; set; }
    }
}
