namespace WebStore.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using WebStore.Data.Common.Repositories;
    using WebStore.Data.Models;
    using WebStore.Services.Data.Contracts;
    using WebStore.Services.Mapping;
    using WebStore.Web.ViewModels.Product;

    public class ProductsService : IProductsService
    {
        private const int MaximumCategoriesPerProduct = 3;
        private const string ImagePath = "products";

        private readonly IDeletableEntityRepository<Product> productsRepository;
        private readonly IRepository<CategoryProduct> categoryProductRepository;
        private readonly ICategoriesService categoriesService;
        private readonly ICategoriesProductsService categoriesProductsService;
        private readonly IImageProcessingService imageProcessing;

        public ProductsService (
            IDeletableEntityRepository<Product> productRepository,
            IRepository<CategoryProduct> categoryProductRepository,
            ICategoriesService categoriesService,
            ICategoriesProductsService categoriesProductsService,
            IImageProcessingService imageProcessing)
        {
            this.productsRepository = productRepository;
            this.categoryProductRepository = categoryProductRepository;
            this.categoriesService = categoriesService;
            this.categoriesProductsService = categoriesProductsService;
            this.imageProcessing = imageProcessing;
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
                AvailableQuantity = inputModel.Quantity,
            };


            await this.productsRepository.AddAsync(product);
            await this.productsRepository.SaveChangesAsync();


            await this.imageProcessing.UploadImageAsync(inputModel.MainImage, ImagePath + "/" + product.Id);

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
                AllCategories = this.categoriesService.GetCategoriesAsKeyValuePairs(),
            };

            var categoryIds = this.categoryProductRepository.AllAsNoTracking()
                .Where(x => x.ProductId == product.Id)
                .Select(x => new
                    {
                        CategoryId = x.CategoryId,
                    })
                .ToList();
            if (categoryIds != null)
            {
                int countOfCategoreis = categoryIds.Count();
                model.FirstCategory = categoryIds[0].CategoryId.ToString();
                if (countOfCategoreis == MaximumCategoriesPerProduct)
                {
                    model.SecondCategory = categoryIds[1].CategoryId.ToString();
                    model.ThirdCategory = categoryIds[2].CategoryId.ToString();
                }
                else if (countOfCategoreis == MaximumCategoriesPerProduct - 1)
                {
                    model.SecondCategory = categoryIds[1].CategoryId.ToString();
                }
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
            product.ModifiedOn = DateTime.UtcNow;
            product.AvailableQuantity = inputModel.Quantity;

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

            if ( inputModel.ThirdCategory != null &&
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
            return this.productsRepository.AllAsNoTrackingWithDeleted()
                .FirstOrDefault(x => x.Id == id);
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
