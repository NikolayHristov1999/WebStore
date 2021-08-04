namespace WebStore.Web.ViewModels.Administration.Categories
{
    using AutoMapper;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using WebStore.Data.Models;
    using WebStore.Services.Mapping;

    public class CategoryFormModel : IMapTo<Category>
    {
        [Required]
        [MinLength(3)]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MinLength(10)]
        public string Description { get; set; }

        [Display(Name = "Image URL")]
        [Required]
        public string ImageUrl { get; set; }

        [IgnoreMap]
        [Display(Name = "Parent Category")]
        public string ParentCategoryId { get; set; }

        [IgnoreMap]
        public IEnumerable<KeyValuePair<string, string>> AllCategories { get; set; }
    }
}
