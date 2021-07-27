namespace WebStore.Web.ViewModels.Categories
{
    using System.Collections.Generic;
    using System.Linq;

    using AutoMapper;
    using AutoMapper.Configuration.Annotations;
    using WebStore.Data.Models;
    using WebStore.Services.Mapping;
    using WebStore.Web.ViewModels.Product;

    public class SingleCategoryOutputModel : BaseCategoryViewModel, IMapFrom<Category>
    {
        public string ImageUrl { get; set; }

        public string Description { get; set; }

        [Ignore]
        public ListProductsViewModel SinglePageProducts { get; set; }

        public ICollection<CategoryListOutputModel> ChildCategories { get; set; }

    }
}
