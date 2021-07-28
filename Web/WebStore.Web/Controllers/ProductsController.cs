namespace WebStore.Web.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using WebStore.Common;
    using WebStore.Data.Models;
    using WebStore.Services.Data.Contracts;
    using WebStore.Web.Infrastructure.Extensions;
    using WebStore.Web.ViewModels.Product;
    using WebStore.Web.ViewModels.Reviews;

    public class ProductsController : Controller
    {
        private readonly ICategoriesService categoriesService;
        private readonly IProductsService productsService;
        private readonly IReviewsService reviewsService;
        private readonly UserManager<ApplicationUser> userManager;

        public ProductsController(
            ICategoriesService categoriesService,
            IProductsService productsService,
            IReviewsService reviewsService,
            UserManager<ApplicationUser> userManager)
        {
            this.categoriesService = categoriesService;
            this.productsService = productsService;
            this.reviewsService = reviewsService;
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
            var queryParams = new Dictionary<string, string>();
            queryParams.Add("search", search);

            var viewModel = new ListProductsViewModel
            {
                ProductsPerPage = GlobalConstants.ProductsPerPage,
                PageNumber = id,
                ProductsCount = this.productsService.GetAll<ProductInListViewModel>(search).Count(),
                Products = products,
                QueryParams = queryParams,
            };

            return this.View(viewModel);
        }

        public async Task<IActionResult> ById(int? id)
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

            var reviews = this.reviewsService.GetProductReviews<ReviewViewModel>((int)id);
            product.ProductReviews = new ListReviewsViewModel
            {
                Reviews = reviews,
            };

            await this.productsService.IncreaseViewsNumberAsync((int)id);

            return this.View(product);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize]
        public async Task<IActionResult> CreateReview(int? id, ReviewFormModel model)
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

            if (!this.ModelState.IsValid)
            {
                return this.RedirectToAction(nameof(this.ById), new { id = id });
            }

            await this.reviewsService.CreateReviewAsync(
                (int)id, model.FullName, model.Content, model.Stars, this.User.GetId());

            return this.RedirectToAction(nameof(this.ById), new { id = id });
        }

        public IActionResult Index()
        {
            return this.RedirectToAction(nameof(this.All));
        }


    }
}
