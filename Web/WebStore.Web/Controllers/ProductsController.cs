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
    using WebStore.Data;
    using WebStore.Data.Models;
    using WebStore.Services.Data;
    using WebStore.Web.ViewModels.Product;

    
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ICategoriesService categoriesService;
        private readonly IProductsService productsService;
        private readonly UserManager<ApplicationUser> userManager;

        public ProductsController(
            ApplicationDbContext context,
            ICategoriesService categoriesService,
            IProductsService productsService,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            this.categoriesService = categoriesService;
            this.productsService = productsService;
            this.userManager = userManager;
        }

        // GET: Products
        public IActionResult Index()
        {
            return this.View(this.productsService.GetAll());
        }

        // GET: Products/Create
        [Authorize]
        public IActionResult Create()
        {
            var viewModel = new ProductInputModel();
            viewModel.Categories = this.categoriesService.GetCategoriesAsKeyValuePairs();
            return this.View(viewModel);
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(ProductInputModel inputModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var user = await this.userManager.GetUserAsync(this.User);
            await this.productsService.CreateAsync(inputModel, user.Id);

            this.TempData["Message"] = "Category added successfully.";
            return this.RedirectToAction(nameof(this.Index));
        }

        // GET: Products/Edit/5
        [Authorize]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var product = this.productsService.GetProductEditModelById((int)id);

            if (product == null)
            {
                return this.NotFound();
            }

            return this.View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, EditProductInputModel productModel)
        {
            if (id != productModel.Id)
            {
                return this.NotFound();
            }

            if (!this.ModelState.IsValid)
            {
                productModel.Categories = this.categoriesService.GetCategoriesAsKeyValuePairs();
                return this.View(productModel);
            }

            await this.productsService.UpdateAsync(id, productModel);
            return this.RedirectToAction(nameof(this.Index));
        }

        // GET: Products/Details/5
        [Authorize]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var product = this.productsService.GetById<DetailsProductOutputModel>((int)id);
            if (product == null)
            {
                return this.NotFound();
            }

            return this.View(product);
        }

        // GET: Products/Delete/5
        [Authorize]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var product = this.productsService.GetById<DeleteProductOutputModel>((int)id);

            if (product == null)
            {
                return this.NotFound();
            }
 
            if (product.IsDeleted)
            {
                return this.NotFound();
            }

            return this.View(product);
        }

        // POST: Products/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.productsService.DeleteProductById(id);
            return this.RedirectToAction(nameof(Index));
        }
    }
}
