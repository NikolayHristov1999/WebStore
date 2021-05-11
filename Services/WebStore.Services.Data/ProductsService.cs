namespace WebStore.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using WebStore.Data.Common.Repositories;
    using WebStore.Data.Models;
    using WebStore.Services.Mapping;
    using WebStore.Web.ViewModels.Product;

    public class ProductsService : IProductsService
    {
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

        public async Task CreateAsync(ProductInputModel inputModel, string userId)
        {
            var product = new Product
            {
                Name = inputModel.Name,
                ShortDescription = inputModel.ShortDescription,
                Price = inputModel.Price,
                ImageUrl = inputModel.ImageUrl,
                AddedByUserId = userId,
            };

            await this.productsRepository.AddAsync(product);
            await this.productsRepository.SaveChangesAsync();

            var isValidated = int.TryParse(inputModel.CategoryId, out int categoryId);
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

        public IEnumerable<T> GetAll<T>()
        {
            var products = this.productsRepository.AllAsNoTracking()
                .To<T>();
            return products;
        }

        public IEnumerable<T> GetAllWithDeleted<T>()
        {
            var products = this.productsRepository.AllAsNoTrackingWithDeleted()
                .To<T>();
            return products;
        }

        public EditProductInputModel GetProductEditModelById(int id)
        {
            var product = this.GetProductById(id);

            if (product == null)
            {
                return null;
            }

            var model = new EditProductInputModel
            {
                Id = product.Id,
                Name = product.Name,
                ShortDescription = product.ShortDescription,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                IsDeleted = product.IsDeleted,
                Categories = this.categoriesService.GetCategoriesAsKeyValuePairs(),
            };

            int? categoryId = this.categoryProductRepository.AllAsNoTracking()
                .FirstOrDefault(x => x.ProductId == product.Id)?.CategoryId;
            if (categoryId != null)
            {
                model.CategoryId = categoryId.ToString();
            }

            return model;
        }

        public async Task UpdateAsync(int id, EditProductInputModel inputModel)
        {
            var product = this.productsRepository.AllWithDeleted().FirstOrDefault(x => x.Id == id);
            product.Name = inputModel.Name;
            product.Price = inputModel.Price;
            product.ShortDescription = inputModel.ShortDescription;
            product.ImageUrl = inputModel.ImageUrl;
            product.IsDeleted = inputModel.IsDeleted;
            product.Categories.Clear();
            product.ModifiedOn = DateTime.UtcNow;

            if (product.IsDeleted)
            {
                product.DeletedOn = DateTime.UtcNow;
            }

            await this.categoriesProductsService.RemoveAllByProductId(id);

            var isValidated = int.TryParse(inputModel.CategoryId, out int categoryId);
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

        private Product GetProductById(int id)
        {
            return this.productsRepository.AllAsNoTrackingWithDeleted()
                .FirstOrDefault(x => x.Id == id);
        }
    }
}
