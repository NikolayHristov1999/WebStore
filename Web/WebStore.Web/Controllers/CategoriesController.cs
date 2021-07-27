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

    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICategoriesService categoriesService;
        private readonly IProductsService productsService;

        public CategoriesController(
            ApplicationDbContext context, 
            ICategoriesService categoriesService,
            IProductsService productsService)
        {
            _context = context;
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
                ProductsCount = this.productsService.GetAllProductsInCategory<ProductInListViewModel>((int)id).Count(),
                Products = this.productsService.GetAllForCategoryPage<ProductInListViewModel>((int)id, page, GlobalConstants.ProductsPerPage),
            };


            return this.View(category);
        }

        [Authorize]
        public IActionResult Index()
        {
            var categories = this.categoriesService.GetAllWithDeleted<DetailsCategoryOutputModel>();
            return this.View(categories);
        }

        [Authorize]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var category = this.categoriesService.GetById<DetailsCategoryOutputModel>((int)id);
            if (category == null)
            {
                return this.NotFound();
            }

            return this.View(category);
        }

        // GET: Categories/Create
        [Authorize]
        public IActionResult Create()
        {
            var categoryViewModel = new CategoryInputModel();
            categoryViewModel.Categories = this.categoriesService.GetCategoriesAsKeyValuePairs();
            return this.View(categoryViewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryInputModel categoryInputModel)
        {

            if (!this.ModelState.IsValid)
            {
                return this.View(categoryInputModel);
            }

            await this.categoriesService.CreateAsync(categoryInputModel);

            this.TempData["Message"] = "Category added successfully.";
            return this.RedirectToAction(nameof(this.Index));

        }

        [Authorize]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var category = this.categoriesService.GetProductEditModelById((int)id);
            if (category == null)
            {
                return this.NotFound();
            }

            return this.View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditCategoryInputModel category)
        {
            if (id != category.Id)
            {
                return this.NotFound();
            }

            if (!this.ModelState.IsValid)
            {
                category.Categories = this.categoriesService.GetCategoriesAsKeyValuePairs((int)id);
                return this.View(category);
            }

            await this.categoriesService.UpdateAsync(id, category);

            return this.RedirectToAction(nameof(this.Index));
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var category = categoriesService.GetById<EditCategoryInputModel>((int)id);

            if (category == null)
            {
                return this.NotFound();
            }

            return this.View(category);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.categoriesService.DeleteCategoryById(id);
            return this.RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
