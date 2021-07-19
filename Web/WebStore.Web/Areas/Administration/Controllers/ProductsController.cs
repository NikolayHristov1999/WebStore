namespace WebStore.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using WebStore.Data.Models;
    using WebStore.Services.Data.Contracts;
    using WebStore.Web.Infrastructure.Extensions;
    using WebStore.Web.ViewModels.Administration.Products;

    public class ProductsController : AdministrationController
    {
        private readonly IProductsService productsService;
        private readonly ICategoriesService categoriesService;
        private readonly UserManager<ApplicationUser> userManager;

        public ProductsController(
            IProductsService productsService,
            ICategoriesService categoriesService,
            UserManager<ApplicationUser> userManager)
        {
            this.productsService = productsService;
            this.categoriesService = categoriesService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = this.User.GetId();
            var model = this.productsService.GetAllForSeller<TableProductViewModel>(userId);
            return this.View(model);
        }

        public IActionResult ById(int? id)
        {
            var userId = this.User.GetId();
            var model = this.productsService.GetEditProductModelById((int)id);

            if (model == null)
            {
                return this.NotFound();
            }

            if (userId != this.productsService.GetProductById((int)id).AddedByUserId)
            {
                return this.Unauthorized();
            }

            return this.View(model);
        }

        public IActionResult Create()
        {
            return View();
        }
    }
}
