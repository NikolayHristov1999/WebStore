namespace WebStore.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using WebStore.Common;
    using WebStore.Data;
    using WebStore.Data.Models;
    using WebStore.Services.Data;
    using WebStore.Services.Data.Contracts;
    using WebStore.Web.ViewModels.Product;

    public class ProductsController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly IProductsService productsService;
        private readonly UserManager<ApplicationUser> userManager;

        public ProductsController(
            ICategoriesService categoriesService,
            IProductsService productsService,
            UserManager<ApplicationUser> userManager)
        {
            this.categoriesService = categoriesService;
            this.productsService = productsService;
            this.userManager = userManager;
        }

        // GET: Products

        public IActionResult All(int id = 1, string search = "")
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            var products = this.productsService.GetAllForSinglePage<ProductInListViewModel>(
                id, GlobalConstants.ProductsPerPage, search);

            var viewModel = new ListProductsViewModel
            {
                ProductsPerPage = GlobalConstants.ProductsPerPage,
                PageNumber = id,
                ProductsCount = this.productsService.GetAll<ProductInListViewModel>(search).Count(),
                Products = products,
            };

            return this.View(viewModel);
        }

        public IActionResult ById(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var product = this.productsService.GetById<SingleProductOutputModel>((int)id);
            if (product == null)
            {
                return this.NotFound();
            }

            this.productsService.IncreaseViewsNumber((int)id);

            return this.View(product);
        }

        public IActionResult Index()
        {
            return this.RedirectToAction(nameof(this.All));
        }

    }
}
