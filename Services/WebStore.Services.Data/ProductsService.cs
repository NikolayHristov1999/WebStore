namespace WebStore.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using WebStore.Common;
    using WebStore.Data.Common.Repositories;
    using WebStore.Data.Models;
    using WebStore.Services.Data.Contracts;
    using WebStore.Services.Mapping;
    using WebStore.Web.ViewModels.Administration.Products;
    using WebStore.Web.ViewModels.Product;

    public class ProductsService : IProductsService
    {
        private const int MaximumCategoriesPerProduct = 3;

        private readonly IDeletableEntityRepository<Product> productsRepository;
        private readonly IRepository<CategoryProduct> categoryProductRepository;
        private readonly ICategoriesService categoriesService;
        private readonly ICategoriesProductsService categoriesProductsService;

        public ProductsService (
            IDeletableEntityRepository<Product> productRepository,
            IRepository<CategoryProduct> categoryProductRepository,
            ICategoriesService categoriesService,
            ICategoriesProductsService categoriesProductsService)
        {
            this.productsRepository = productRepository;
            this.categoryProductRepository = categoryProductRepository;
            this.categoriesService = categoriesService;
            this.categoriesProductsService = categoriesProductsService;
        }

        public async Task CreateAsync(CreateProductViewModel inputModel, string userId)
        {
            var product = AutoMapperConfig.MapperInstance.Map<Product>(inputModel);
            product.AddedByUserId = userId;

            await this.productsRepository.AddAsync(product);
            await this.productsRepository.SaveChangesAsync();


            var isValidated = int.TryParse(inputModel.FirstCategory, out int categoryId);
            if (isValidated && this.categoriesService.GetCategoryName(categoryId) != null)
            {
                var categoryProduct = new CategoryProduct
                {
                    CategoryId = categoryId,
                    ProductId = product.Id,
                };

                await this.categoryProductRepository.AddAsync(categoryProduct);
                await this.categoryProductRepository.SaveChangesAsync();
            }
        }

        public IEnumerable<T> GetAll<T>(string searchString = "")
        {
            var products = this.productsRepository.AllAsNoTracking();
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                products = products.Where(x => x.Name.Contains(searchString));
            }

            return products.OrderBy(x => x.Id).To<T>().ToList();
        }

        public IEnumerable<T> GetAllWithDeleted<T>(string searchString = "")
        {
            var products = this.productsRepository.AllAsNoTrackingWithDeleted();
            if (!string.IsNullOrWhiteSpace(searchString))
            {
                products = products.Where(x => x.Name.Contains(
                    searchString,
                    StringComparison.InvariantCultureIgnoreCase));
            }

            return products.To<T>();
        }

        public IEnumerable<T> GetAllForSinglePage<T>(
            int page = 1,
            int productsPerPage = GlobalConstants.ProductsPerPage,
            string searchString = "")
        {
            return this.GetAll<T>(searchString)
                .Skip((page - 1) * productsPerPage)
                .Take(productsPerPage);
        }

        public IEnumerable<T> GetAllForCategoryPage<T>(
            int categoryId,
            int page = 1,
            int productsPerPage = GlobalConstants.ProductsPerPage)
        {
            return this.GetAllProductsInCategory<T>(categoryId)
                .Skip((page - 1) * productsPerPage)
                .Take(productsPerPage);
        }

        public IEnumerable<T> GetAllProductsInCategory<T>(int categoryId)
        {
            return this.productsRepository.AllAsNoTracking()
                .Where(x => x.Categories.Any(y => y.CategoryId == categoryId))
                .To<T>()
                .ToList();
        }

        public EditProductViewModel GetEditProductModelById(int id)
        {
            var product = this.productsRepository.AllWithDeleted()
                .Where(x => x.Id == id)
                .To<EditProductViewModel>()
                .FirstOrDefault();

            if (product == null)
            {
                return null;
            }

            product.AllCategories = this.categoriesService.GetCategoriesAsKeyValuePairs();

            var categoryIds = this.categoriesService.GetCategoriesForProduct(id)
                .Select(x => new
                {
                    CategoryId = x.Id,
                })
                .ToList();

            if (categoryIds != null)
            {
                int countOfCategoreis = categoryIds.Count();
                product.FirstCategory = categoryIds[0].CategoryId.ToString();
                if (countOfCategoreis == MaximumCategoriesPerProduct)
                {
                    product.SecondCategory = categoryIds[1].CategoryId.ToString();
                    product.ThirdCategory = categoryIds[2].CategoryId.ToString();
                }
                else if (countOfCategoreis == MaximumCategoriesPerProduct - 1)
                {
                    product.SecondCategory = categoryIds[1].CategoryId.ToString();
                }
            }

            return product;
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
            product.IsDeleted = inputModel.IsDeleted;
            product.ModifiedOn = DateTime.UtcNow;
            product.AvailableQuantity = inputModel.AvailableQuantity;

            if (product.IsDeleted)
            {
                product.DeletedOn = DateTime.UtcNow;
            }

            await this.categoriesProductsService.RemoveAllByProductId(id);

            await this.UpdateCategoryProduct(id, inputModel.FirstCategory);

            if (inputModel.SecondCategory != null &&
                !inputModel.SecondCategory.Equals(inputModel.FirstCategory))
            {
                await this.UpdateCategoryProduct(id, inputModel.SecondCategory);
            }

            if (inputModel.ThirdCategory != null &&
                !inputModel.ThirdCategory.Equals(inputModel.FirstCategory) && 
                !inputModel.ThirdCategory.Equals(inputModel.SecondCategory))
            {
                await this.UpdateCategoryProduct(id, inputModel.ThirdCategory);
            }

            await this.productsRepository.SaveChangesAsync();
        }

        public T GetById<T>(int id)
        {
            var product = this.productsRepository.AllAsNoTrackingWithDeleted()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return product;
        }

        public async Task DeleteProductById(int id)
        {
            var product = this.GetProductById(id);

            this.productsRepository.Delete(product);
            await this.productsRepository.SaveChangesAsync();
        }

        public Product GetProductById(int id)
        {
            return this.productsRepository.AllWithDeleted()
                .FirstOrDefault(x => x.Id == id);
        }


        public async Task IncreaseViewsNumber(int id)
        {
            var product = this.GetProductById(id);

            if (product == null)
            {
                return;
            }

            product.Views++;

            await this.productsRepository.SaveChangesAsync();
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

        private async Task UpdateCategoryProduct(int productId, string newCategoryId)
        {
            if (newCategoryId != null)
            {
                var isValidated = int.TryParse(newCategoryId, out int categoryId);
                if (isValidated && this.categoriesService.GetCategoryName(categoryId) != null)
                {
                    var categoryProduct = new CategoryProduct
                    {
                        CategoryId = categoryId,
                        ProductId = productId,
                    };

                    await this.categoryProductRepository.AddAsync(categoryProduct);
                    await this.categoryProductRepository.SaveChangesAsync();
                }
            }
        }

    }
}
