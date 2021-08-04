namespace WebStore.Web.ViewModels.Categories
{
    using System.Collections.Generic;

    using AutoMapper.Configuration.Annotations;
    using Ganss.XSS;
    using WebStore.Data.Models;
    using WebStore.Services.Mapping;
    using WebStore.Web.ViewModels.Product;

    public class SingleCategoryOutputModel : BaseCategoryViewModel, IMapFrom<Category>
    {
        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public string SanitizedDescription => new HtmlSanitizer().Sanitize(this.Description);

        [Ignore]
        public ListProductsViewModel SinglePageProducts { get; set; }

        public ICollection<CategoryListOutputModel> ChildCategories { get; set; }
    }
}
