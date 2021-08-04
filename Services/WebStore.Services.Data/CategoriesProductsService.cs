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

    public class CategoriesProductsService : ICategoriesProductsService
    {
        private readonly IRepository<CategoryProduct> categoryProductRepository;
        private readonly ICategoriesService categoriesService;

        public CategoriesProductsService(
            IRepository<CategoryProduct> categoryProductRepository,
            ICategoriesService categoriesService)
        {
            this.categoryProductRepository = categoryProductRepository;
            this.categoriesService = categoriesService;
        }

        public async Task RemoveAllByProductId(int id)
        {
            var productCategories = this.categoryProductRepository.AllAsNoTracking()
                .Where(x => x.ProductId == id);

            foreach (var productCategory in productCategories)
            {
                this.categoryProductRepository.Delete(productCategory);
            }

            await this.categoryProductRepository.SaveChangesAsync();
        }

        public async Task AddAsync(int productId, int categoryId)
        {
            if (this.categoriesService.GetCategoryName(categoryId) != null)
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
