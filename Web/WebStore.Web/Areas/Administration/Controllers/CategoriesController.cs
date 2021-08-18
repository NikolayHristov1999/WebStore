namespace WebStore.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using WebStore.Common;
    using WebStore.Services.Data.Contracts;
    using WebStore.Web.ViewModels.Administration.Categories;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    public class CategoriesController : AdministrationController
    {
        private readonly ICategoriesService categoriesService;

        public CategoriesController(
            ICategoriesService categoriesService)
        {
            this.categoriesService = categoriesService;
        }

        public IActionResult Index()
        {
            var model = this.categoriesService.GetAllWithDeleted<CategoryInTableViewModel>();
            return this.View(model);
        }

        public IActionResult Create()
        {
            var model = new CategoryFormModel
            {
                AllCategories = this.categoriesService.GetCategoriesAsKeyValuePairs(),
            };

            return this.View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Create(CategoryFormModel model)
        {
            if (!this.ModelState.IsValid)
            {
                model.AllCategories = this.categoriesService.GetCategoriesAsKeyValuePairs();
                return this.View();
            }

            await this.categoriesService.CreateAsync(model);

            return this.RedirectToAction(nameof(this.Index));
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var model = this.categoriesService.ByIdWithDeleted<EditCategoryViewModel>((int)id);
            model.AllCategories = this.categoriesService.GetCategoriesAsKeyValuePairs();

            if (model == null)
            {
                return this.NotFound();
            }

            return this.View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Edit(int? id, EditCategoryViewModel model)
        {
            if (id != model.Id)
            {
                return this.NotFound();
            }

            if (!this.ModelState.IsValid)
            {
                model.AllCategories = this.categoriesService.GetCategoriesAsKeyValuePairs((int)id);
                return this.View(model);
            }

            await this.categoriesService.UpdateAsync(
                model.Id,
                model.Name,
                model.Description,
                model.ImageUrl,
                model.ParentCategory,
                model.IsDeleted);

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}
