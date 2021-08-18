namespace WebStore.Web.Tests.Controllers.Administration
{
    using MyTested.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using WebStore.Data.Models;
    using WebStore.Web.Areas.Administration.Controllers;
    using WebStore.Web.ViewModels.Administration.Products;
    using Xunit;

    using static WebStore.Web.Tests.Data.Products;

    public class ProductsControllerTest
    {
        [Fact]
        public void IndexShouldReturnViewModelWithProducts()
            => MyController<ProductsController>
                .Instance(controller => controller
                    .WithUser()
                    .WithData(GetTenProductsWithDealerId(TestUser.Identifier)))
                .Calling(c => c.Index())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<IEnumerable<TableProductViewModel>>()
                    .Passing(model => model.Count() == 10));

        [Fact]
        public void CreateShouldReturnViewWithModel()
            => MyController<ProductsController>
                .Instance()
                .Calling(c => c.Create())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<CreateProductFormModel>());

        [Theory]
        [InlineData("ProductName", 13, "Short Descrrr", "Descrrrrrrr", "China", "China", @"www.test.com/testimg.jpeg")]
        public void PostCreateShouldReturnRedirectToIndexIfSuccessful(
            string name,
            decimal price,
            string shortDescription,
            string description,
            string storedInCountry,
            string madeIn,
            string imageUrl)
            => MyController<ProductsController>
                .Instance(controller => controller
                    .WithUser())
                .Calling(c => c.Create(new CreateProductFormModel
                {
                    Name = name,
                    Price = price,
                    ShortDescription = shortDescription,
                    Description = description,
                    StoredInCountry = storedInCountry,
                    MadeIn = madeIn,
                    ImageUrl = imageUrl,
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .ValidModelState()
                .Data(data => data
                    .WithSet<Product>(products => products
                        .Any(product =>
                            product.Name == name &&
                            product.Price == price &&
                            product.ShortDescription == shortDescription &&
                            product.Description == description &&
                            product.StoredInCountry == storedInCountry &&
                            product.MadeIn == madeIn &&
                            product.ImageUrl == imageUrl)))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect.
                    ToAction("Index"));

        [Fact]
        public void ByIdShouldReturnCorrectProductViewModel()
            => MyController<ProductsController>
                .Instance(controller => controller
                    .WithUser()
                    .WithData(GetTenProductsWithDealerId(TestUser.Identifier)))
                .Calling(c => c.ById(1))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<EditProductViewModel>());

        [Theory]
        [InlineData("ProductName", 13, "Short Descrrr", "Descrrrrrrr", "China", "China", @"www.test.com/testimg.jpeg")]
        public void PostByIdShouldReturnRedirectToIndexIfSuccessful(
            string name,
            decimal price,
            string shortDescription,
            string description,
            string storedInCountry,
            string madeIn,
            string imageUrl)
            => MyController<ProductsController>
                .Instance(controller => controller
                    .WithUser()
                    .WithData(GetTenProductsWithDealerId(TestUser.Identifier)))
                .Calling(c => c.ById(1, new EditProductViewModel
                {
                    Id = 1,
                    Name = name,
                    Price = price,
                    ShortDescription = shortDescription,
                    Description = description,
                    StoredInCountry = storedInCountry,
                    MadeIn = madeIn,
                    ImageUrl = imageUrl,
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .ValidModelState()
                .Data(data => data
                    .WithSet<Product>(products => products
                        .Any(product =>
                            product.Name == name &&
                            product.Price == price &&
                            product.ShortDescription == shortDescription &&
                            product.Description == description &&
                            product.StoredInCountry == storedInCountry &&
                            product.MadeIn == madeIn &&
                            product.ImageUrl == imageUrl)))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect.
                    ToAction("Index"));
    }
}
