namespace WebStore.Web.ViewModels.Administration.Products
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using AutoMapper;
    using AutoMapper.Configuration.Annotations;
    using WebStore.Data.Models;
    using WebStore.Services.Mapping;

    public class EditProductViewModel : IMapFrom<Product>, IMapTo<Product>, IHaveCustomMappings
    {
        public int Id { get; set; }

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

        [Display(Name = "Made in")]
        public string MadeIn { get; set; }

        [Display(Name = "Stored in country")]
        public string StoredInCountry { get; set; }

        [Display(Name = "Deleted")]
        public bool IsDeleted { get; set; }

        [Display(Name = "Category")]
        public IList<int?> CategoriesId { get; set; } = new List<int?>();

        [Ignore]
        public IEnumerable<KeyValuePair<string, string>> AllCategories { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Product, EditProductViewModel>()
                .ForMember(x => x.CategoriesId, opt => opt.MapFrom(y => y.Categories.Select(pc => (int?)pc.CategoryId)));
        }
    }
}
