namespace WebStore.Web.Controllers
{
    using System.Diagnostics;

    using Microsoft.AspNetCore.Mvc;
    using WebStore.Common;
    using WebStore.Services.Data.Contracts;
    using WebStore.Web.ViewModels;
    using WebStore.Web.ViewModels.Home;
    using WebStore.Web.ViewModels.Product;

    public class HomeController : BaseController
    {
        private readonly IProductsService productsService;

        public HomeController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        public IActionResult Index()
        {
            var model = new HomeIndexPageProductsViewModel
            {
                NewProducts = this.productsService
                    .LatestProducts<ProductInListViewModel>(GlobalConstants.ProductPerCategoryInHomePage),
                TopProducts = this.productsService
                    .MostVisitedProducts<ProductInListViewModel>(GlobalConstants.ProductPerCategoryInHomePage),
            };

            return this.View(model);
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}
