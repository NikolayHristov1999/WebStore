namespace WebStore.Web.Tests.Controllers
{
    using System.Collections.Generic;
    using System.Linq;

    using MyTested.AspNetCore.Mvc;
    using WebStore.Web.Controllers;
    using WebStore.Web.ViewModels.Categories;
    using Xunit;

    using static WebStore.Web.Tests.Data.Categories;
    using static WebStore.Web.Tests.Data.Products;

    public class CategoriesControllerTest
    {
        [Fact]
        public void AllShouldReturnAllRootCategories()
            => MyController<CategoriesController>
                .Instance(controller => controller
                    .WithData(SixCategories))
                .Calling(c => c.All())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<IEnumerable<CategoryListOutputModel>>()
                    .Passing(model => model.Count() == 6));

        [Fact]
        public void ByIdShouldReturnAllProductsInCategory()
            => MyController<CategoriesController>
                .Instance(controller => controller
                    .WithData(FifteenProducts)
                    .WithData(SixCategories))
                .Calling(c => c.ById(1, 1))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<SingleCategoryOutputModel>());
    }
}
