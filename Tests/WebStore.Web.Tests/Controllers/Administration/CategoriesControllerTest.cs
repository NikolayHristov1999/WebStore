namespace WebStore.Web.Tests.Controllers.Administration
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using MyTested.AspNetCore.Mvc;
    using WebStore.Data.Models;
    using WebStore.Web.Areas.Administration.Controllers;
    using WebStore.Web.ViewModels.Administration.Categories;
    using Xunit;

    using static WebStore.Web.Tests.Data.Categories;

    public class CategoriesControllerTest
    {
        [Fact]
        public void IndexShouldReturnViewModelWithCategories()
            => MyController<CategoriesController>
                .Instance(controller => controller
                    .WithUser()
                    .WithData(SixCategories))
                .Calling(c => c.Index())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<IEnumerable<CategoryInTableViewModel>>()
                    .Passing(model => model.Count() == 6));

        [Fact]
        public void CreateShouldReturnViewWithModel()
            => MyController<CategoriesController>
                .Instance()
                .Calling(c => c.Create())
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<CategoryFormModel>());

        [Theory]
        [InlineData("ProductName", "Description", @"www.test.com/testimg.jpeg")]
        public void PostCreateShouldReturnRedirectToIndexIfSuccessful(
            string name,
            string description,
            string imageUrl)
            => MyController<CategoriesController>
                .Instance(controller => controller
                    .WithUser())
                .Calling(c => c.Create(new CategoryFormModel
                {
                    Name = name,
                    Description = description,
                    ImageUrl = imageUrl,
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .ValidModelState()
                .Data(data => data
                    .WithSet<Category>(categories => categories
                        .Any(category =>
                            category.Name == name &&
                            category.Description == description &&
                            category.ImageUrl == imageUrl)))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect.
                    ToAction("Index"));

        [Fact]
        public void EditShouldReturnCorrectCategoryViewModel()
            => MyController<CategoriesController>
                .Instance(controller => controller
                    .WithUser()
                    .WithData(SixCategories))
                .Calling(c => c.Edit(1))
                .ShouldReturn()
                .View(view => view
                    .WithModelOfType<EditCategoryViewModel>());

        [Theory]
        [InlineData("ProductName", "Description", @"www.test.com/testimg.jpeg")]
        public void PostEditShouldReturnRedirectToIndexIfSuccessful(
            string name,
            string description,
            string imageUrl)
            => MyController<CategoriesController>
                .Instance(controller => controller
                    .WithUser()
                    .WithData(SixCategories))
                .Calling(c => c.Edit(1, new EditCategoryViewModel
                {
                    Id = 1,
                    Name = name,
                    Description = description,
                    ImageUrl = imageUrl,
                }))
                .ShouldHave()
                .ActionAttributes(attributes => attributes
                    .RestrictingForHttpMethod(HttpMethod.Post))
                .ValidModelState()
                .Data(data => data
                    .WithSet<Category>(categories => categories
                        .Any(category =>
                            category.Name == name &&
                            category.Description == description &&
                            category.ImageUrl == imageUrl)))
                .AndAlso()
                .ShouldReturn()
                .Redirect(redirect => redirect.
                    ToAction("Index"));
    }
}
