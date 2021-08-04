namespace WebStore.Web.ViewModels.Administration.Categories
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using Ganss.XSS;
    using WebStore.Data.Models;
    using WebStore.Services.Mapping;

    public class EditCategoryViewModel : IMapFrom<Category>, IHaveCustomMappings
    {
        public int Id { get; set; }

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

        public bool IsDeleted { get; set; }

        [Display(Name = "Parent Category")]
        public string ParentCategory { get; set; }

        [IgnoreMap]
        public IEnumerable<KeyValuePair<string, string>> AllCategories { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Category, EditCategoryViewModel>()
                .ForMember(x => x.ParentCategory, opt => opt
                    .MapFrom(y => y.ParentCategory != null ? y.ParentCategoryId.ToString() : "None"));
        }
    }
}
