namespace WebStore.Web.ViewModels.Categories
{
    using AutoMapper;
    using System;
    using System.ComponentModel.DataAnnotations;
    using WebStore.Data.Models;
    using WebStore.Services.Mapping;

    public class DetailsCategoryOutputModel : BaseCategoryOutputModel, IMapFrom<Category>, IHaveCustomMappings
    {
        [Display(Name = "Image")]
        public string ImageUrl { get; set; }

        public string Description { get; set; }

        [Display(Name = "Parent Category Name")]
        public string ParentCategory { get; set; }

        [Display(Name = "Created On")]
        public DateTime CreatedOn { get; set; }

        [Display(Name = "Last Modified On")]
        public DateTime? ModifiedOn { get; set; }

        [Display(Name = "Deleted")]
        public bool IsDeleted { get; set; }

        [Display(Name = "Deleted On")]
        public DateTime? DeletedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Category, DetailsCategoryOutputModel>()
                .ForMember(x => x.ParentCategory, opt => opt.MapFrom(x => x.ParentCategory.Name ?? "None"));
        }
    }
}
