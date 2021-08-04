namespace WebStore.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using WebStore.Common;
    using WebStore.Data.Common.Repositories;
    using WebStore.Data.Models;
    using WebStore.Services.Data.Contracts;
    using WebStore.Services.Mapping;
    using WebStore.Web.ViewModels.Administration.Products;

    using static WebStore.Data.Common.DataConstants;

    public class ProductsService : IProductsService
    {
        private const int MaximumCategoriesPerProduct = 3;

        private readonly IDeletableEntityRepository<Product> productsRepository;
        private readonly IRepository<CategoryProduct> categoryProductRepository;
        private readonly ICategoriesService categoriesService;
        private readonly ICategoriesProductsService categoriesProductsService;
        private readonly IDealerService salesmanService;

        public ProductsService(
            IDeletableEntityRepository<Product> productRepository,
            IRepository<CategoryProduct> categoryProductRepository,
            ICategoriesService categoriesService,
            ICategoriesProductsService categoriesProductsService,
            IDealerService salesmanService)
        {
            this.productsRepository = productRepository;
            this.categoryProductRepository = categoryProductRepository;
            this.categoriesService = categoriesService;
            this.categoriesProductsService = categoriesProductsService;
            this.salesmanService = salesmanService;
        }

        public async Task CreateAsync(CreateProductFormModel inputModel, string userId)
        {
            var product = AutoMapperConfig.MapperInstance.Map<Product>(inputModel);
            product.AddedByUserId = userId;

            await this.productsRepository.AddAsync(product);
            await this.productsRepository.SaveChangesAsync();

            for (int i = 0; i < inputModel.CategoriesId.Count; i++)
            {
                var categoryId = inputModel.CategoriesId[i];
                if (categoryId != null && inputModel.CategoriesId.IndexOf(categoryId) == i)
                {
                    await this.categoriesProductsService.AddAsync(product.Id, (int)categoryId);
                }
            }
        }

        public async Task UpdateAsync(int id, EditProductViewModel inputModel)
        {
            var product = this.productsRepository.AllWithDeleted().FirstOrDefault(x => x.Id == id);
            product.Name = inputModel.Name;
            product.Price = inputModel.Price;
            product.ShortDescription = inputModel.ShortDescription;
            product.Description = inputModel.Description;
            product.MadeIn = inputModel.MadeIn;
            product.StoredInCountry = inputModel.StoredInCountry;
            product.ImageUrl = inputModel.ImageUrl;
            product.AvailableQuantity = inputModel.AvailableQuantity;

            if (product.IsDeleted != inputModel.IsDeleted)
            {
                if (inputModel.IsDeleted)
                {
                    this.productsRepository.Delete(product);
                }
                else
                {
                    product.IsDeleted = inputModel.IsDeleted;
                }
            }

            await this.categoriesProductsService.RemoveAllByProductId(id);

            for (int i = 0; i < inputModel.CategoriesId.Count; i++)
            {
                var categoryId = inputModel.CategoriesId[i];
                if (categoryId != null && inputModel.CategoriesId.IndexOf(categoryId) == i)
                {
                    await this.categoriesProductsService.AddAsync(id, (int)categoryId);
                }
            }

            await this.productsRepository.SaveChangesAsync();
        }

        public T ById<T>(int id)
        {
            var product = this.productsRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return product;
        }

        public T ByIdWithDeleted<T>(int id)
        {
            var product = this.productsRepository.AllAsNoTrackingWithDeleted()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return product;
        }

        // TO DO: Finish  the logic where only allowed delear's product are shown
        public IEnumerable<T> AllAvailableForPurchase<T>(string searchString = "")
        {
            var products = this.productsRepository.AllAsNoTracking();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                products = products.Where(x => x.Name.Contains(searchString));
            }

            return products.OrderBy(x => x.Id).To<T>();
        }

        /// <summary>
        ///     Get all products that are not deleted.
        /// </summary>
        /// <typeparam name="T">The view model to be returned.</typeparam>
        /// <param name="searchString">If there is a search query</param>
        /// <returns>Enumerable of the model type.</returns>
        public IEnumerable<T> All<T>(string searchString = "")
        {
            var products = this.productsRepository.AllAsNoTracking();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                products = products.Where(x => x.Name.Contains(searchString));
            }

            return products.OrderBy(x => x.Id).To<T>();
        }

        public IEnumerable<T> AllWithDeleted<T>(string searchString = "")
        {
            var products = this.productsRepository.AllAsNoTrackingWithDeleted();

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                products = products.Where(x => x.Name.Contains(searchString));
            }

            return products.OrderBy(x => x.Id).To<T>();
        }

        /// <summary>
        ///     Get the products for the current page.
        /// </summary>
        /// <typeparam name="T">Model Type </typeparam>
        /// <param name="page">The current page the user is requesting. Default: 1.</param>
        /// <param name="productsPerPage">The number of products per page.</param>
        /// <param name="searchString">If there is a search qeuery string.</param>
        /// <returns></returns>
        public IEnumerable<T> AllForSinglePage<T>(
            int page = 1,
            int productsPerPage = GlobalConstants.ProductsPerPage,
            string searchString = "")
        {
            return this.All<T>(searchString)
                .Skip((page - 1) * productsPerPage)
                .Take(productsPerPage);
        }

        public IEnumerable<T> AllProductsInCategory<T>(int categoryId)
        {
            return this.productsRepository.AllAsNoTracking()
                .Where(x => x.Categories.Any(y => y.CategoryId == categoryId))
                .To<T>();
        }

        public IEnumerable<T> AllForCategoryPage<T>(
            int categoryId,
            int page = 1,
            int productsPerPage = GlobalConstants.ProductsPerPage)
        {
            return this.AllProductsInCategory<T>(categoryId)
                .Skip((page - 1) * productsPerPage)
                .Take(productsPerPage);
        }

        public async Task DeleteProductById(int id)
        {
            var product = this.GetProductById(id);

            this.productsRepository.Delete(product);
            await this.productsRepository.SaveChangesAsync();
        }

        public async Task IncreaseViewsNumberAsync(int id)
        {
            var product = this.GetProductById(id);

            if (product == null)
            {
                return;
            }

            product.Views++;

            await this.productsRepository.SaveChangesAsync();
        }

        public bool IsUserOwner(string userId, int productId)
        {
            return this.productsRepository.AllAsNoTrackingWithDeleted()
                .Any(x => x.AddedByUserId == userId && x.Id == productId);
        }

        public IEnumerable<T> GetAllForSeller<T>(string userId)
        {
            return this.productsRepository.AllWithDeleted()
                .Where(x => x.AddedByUserId == userId)
                .OrderBy(x => x.IsDeleted)
                .ThenByDescending(x => x.CreatedOn)
                .To<T>()
                .ToList();
        }

        public Product GetProductById(int id)
        {
            return this.productsRepository.AllWithDeleted()
                .FirstOrDefault(x => x.Id == id);
        }
    }
}
