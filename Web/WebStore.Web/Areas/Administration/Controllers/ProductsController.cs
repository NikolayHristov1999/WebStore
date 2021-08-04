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

        public IActionResult Index()
        {
            var userId = this.User.GetId();
            var model = this.productsService.GetAllForSeller<TableProductViewModel>(userId);
            return this.View(model);
        }

        public IActionResult Create()
        {
            var model = new CreateProductFormModel
            {
                AllCategories = this.categoriesService.GetCategoriesAsKeyValuePairs(),
            };

            return this.View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(CreateProductFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            await this.productsService.CreateAsync(model, this.User.GetId());

            this.TempData["Message"] = "Product added successfully.";
            return this.RedirectToAction(nameof(this.Index));
        }

        public IActionResult ById(int? id)
        {
            var userId = this.User.GetId();
            var model = this.productsService.ByIdWithDeleted<EditProductViewModel>((int)id);
            model.AllCategories = this.categoriesService.GetCategoriesAsKeyValuePairs();

            if (model == null)
            {
                return this.NotFound();
            }

            if (!this.productsService.IsUserOwner(userId, (int)id))
            {
                return this.Unauthorized();
            }

            return this.View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ById(int? id, EditProductViewModel model)
        {
            var userId = this.User.GetId();

            var currentProductState = this.productsService.ByIdWithDeleted<EditProductViewModel>((int)id);
            if (currentProductState == null)
            {
                return this.NotFound();
            }

            if (!this.productsService.IsUserOwner(userId, (int)id))
            {
                return this.Unauthorized();
            }

            if (!this.ModelState.IsValid)
            {
                model.AllCategories = this.categoriesService.GetCategoriesAsKeyValuePairs();
                return this.View(model);
            }

            await this.productsService.UpdateAsync((int)id, model);
            return this.RedirectToAction(nameof(this.Index));

        }

    }
}
