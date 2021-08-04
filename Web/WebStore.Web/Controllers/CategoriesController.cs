namespace WebStore.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using WebStore.Common;
    using WebStore.Data;
    using WebStore.Data.Models;
    using WebStore.Services.Data;
    using WebStore.Services.Data.Contracts;
    using WebStore.Web.ViewModels.Categories;
    using WebStore.Web.ViewModels.Product;

    public class CategoriesController : BaseController
    {
        private readonly ICategoriesService categoriesService;
        private readonly IProductsService productsService;

        public CategoriesController(
            ICategoriesService categoriesService,
            IProductsService productsService)
        {
            this.categoriesService = categoriesService;
            this.productsService = productsService;
        }

        public IActionResult All()
        {
            return this.View(this.categoriesService.GetAllRootCategories<CategoryListOutputModel>());
        }

        public IActionResult ById(int? id, int page = 1)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var category = this.categoriesService.GetById<SingleCategoryOutputModel>((int)id);
            if (category == null)
            {
                return this.NotFound();
            }

            category.SinglePageProducts = new ListProductsViewModel
            {
                ProductsPerPage = GlobalConstants.ProductsPerPage,
                PageNumber = page,
                ProductsCount = this.productsService.AllProductsInCategory<ProductInListViewModel>((int)id).Count(),
                Products = this.productsService.AllForCategoryPage<ProductInListViewModel>((int)id, page, GlobalConstants.ProductsPerPage),
            };

            return this.View(category);
        }
    }
}
