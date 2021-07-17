namespace WebStore.Web.Areas.Administration.Controllers
{
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.Threading.Tasks;
    using WebStore.Data.Models;
    using WebStore.Services.Data.Contracts;
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
            var user = await this.userManager.GetUserAsync(this.User);
            var model = this.productsService.GetAllForSeller<TableProductViewModel>(user.Id);
            return this.View(model);
        }

        public async Task<IActionResult> ById(int? id)
        {
            var user = await this.userManager.GetUserAsync(this.User);
            var model = this.productsService.GetEditProductModelById((int)id);

            if (model == null)
            {
                return this.NotFound();
            }

            if (user.Id != this.productsService.GetProductById((int)id).AddedByUserId)
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
