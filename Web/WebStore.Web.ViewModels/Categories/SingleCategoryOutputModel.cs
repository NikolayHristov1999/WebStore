namespace WebStore.Web.ViewModels.Categories
{
    using AutoMapper;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using WebStore.Data.Models;
    using WebStore.Services.Mapping;
    using WebStore.Web.ViewModels.Product;

    public class SingleCategoryOutputModel : BaseCategoryOutputModel, IMapFrom<Category>, IHaveCustomMappings
    {
        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public ICollection<ListProductOutputModel> Products { get; set; }

        public ICollection<CategoryListOutputModel> ChildCategories { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Category, SingleCategoryOutputModel>()
                .ForMember(x => x.Products, opt => opt.MapFrom(x => x.Products.Select(y => y.Product)));
        }
    }
}
