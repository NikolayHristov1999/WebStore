using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebStore.Data;
using WebStore.Data.Models;
using WebStore.Services.Data;
using WebStore.Web.ViewModels.Categories;

namespace WebStore.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICategoriesService categoriesService;

        public CategoriesController(ApplicationDbContext context, ICategoriesService categoriesService)
        {
            _context = context;
            this.categoriesService = categoriesService;
        }

        public IActionResult All()
        {
            return this.View(this.categoriesService.GetAllRootCategories<CategoryListOutputModel>());
        }

        // GET : Categories/ById/5
        public IActionResult ById(int? id)
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

            return this.View(category);
        }

        // GET: Categories
        [Authorize]
        public IActionResult Index()
        {
            var categories = this.categoriesService.GetAllWithDeleted<DetailsCategoryOutputModel>();
            return this.View(categories);
        }

        // GET: Categories/Details/5
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

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Categories/Edit/5
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
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
