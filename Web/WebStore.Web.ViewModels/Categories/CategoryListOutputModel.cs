namespace WebStore.Web.ViewModels.Categories
{
    using WebStore.Data.Models;
    using WebStore.Services.Mapping;

    public class CategoryListOutputModel : BaseCategoryOutputModel, IMapFrom<Category>
    {
        public string ImageUrl { get; set; }
    }
}
