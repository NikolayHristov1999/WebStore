namespace WebStore.Web.ViewModels.Categories
{
    using WebStore.Data.Models;
    using WebStore.Services.Mapping;

    public class CategoryListOutputModel : BaseCategoryViewModel, IMapFrom<Category>
    {
        public string ImageUrl { get; set; }
    }
}
