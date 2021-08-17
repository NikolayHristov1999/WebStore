namespace WebStore.Web.Tests.Controllers
{
    using MyTested.AspNetCore.Mvc;
    using System.Linq;
    using WebStore.Common;
    using WebStore.Web.Controllers;
    using WebStore.Web.ViewModels.Product;
    using WebStore.Web.ViewModels.Reviews;
    using Xunit;

    using static Data.Products;

    public class ProductsControllerTest
    {
        [Fact]
        public void IndexShouldReturnView()
            => MyController<ProductsController>
                .Instance()
                .Calling(c => c.Index())
                .ShouldReturn()
                .RedirectToAction("All");

        [Fact]
        public void AllShouldReturnViewModelWithProducts()
            => MyController<ProductsController>
                .Instance(controller => controller
                    .WithData(FifteenProducts))
                .Calling(c => c.All(1, string.Empty))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<ListProductsViewModel>()
                    .Passing(model => model.Products.Count() == GlobalConstants.ProductsPerPage));

        [Fact]
        public void ByIdShouldReturnCorrectProductViewModel()
            => MyController<ProductsController>
                .Instance(controller => controller
                    .WithData(FifteenProducts))
                .Calling(c => c.ById(1))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<SingleProductViewModel>());

        [Fact]
        public void CreateReviewShouldReturnToRedirectToById()
            => MyController<ProductsController>
                .Instance(controller => controller
                    .WithData(FifteenProducts))
                .Calling(c => c.CreateReview(1, new ReviewFormModel
                {
                    Stars = 5,
                    Content = "Test",
                    FullName = "Testt",
                }))
                .ShouldReturn()
                .RedirectToAction("ById");
    }
}
