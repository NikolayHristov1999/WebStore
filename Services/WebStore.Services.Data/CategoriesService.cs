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
    using WebStore.Web.ViewModels.Administration.Categories;
    using WebStore.Web.ViewModels.Categories;
    using WebStore.Web.ViewModels.Product;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;

        public CategoriesService(
            IDeletableEntityRepository<Category> categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public async Task<int> CreateAsync(CategoryFormModel model)
        {
            var category = AutoMapperConfig.MapperInstance.Map<Category>(model);

            var isValidated = int.TryParse(model.ParentCategoryId, out int categoryId);

            if (isValidated && this.GetCategoryName(categoryId) != null)
            {
                category.ParentCategoryId = categoryId;
            }

            await this.categoriesRepository.AddAsync(category);
            await this.categoriesRepository.SaveChangesAsync();

            return category.Id;
        }

        public async Task UpdateAsync(
            int id,
            string name,
            string description,
            string imageUrl,
            string parentCategoryId,
            bool isDeleted)
        {
            var category = this.categoriesRepository.AllWithDeleted().FirstOrDefault(x => x.Id == id);
            category.Name = name;
            category.Description = description;
            category.ImageUrl = imageUrl;

            var isValidated = int.TryParse(parentCategoryId, out int categoryId);

            if (isValidated && this.GetCategoryName(categoryId) != null)
            {
                category.ParentCategoryId = categoryId;
            }
            else
            {
                category.ParentCategoryId = null;
            }

            if (category.IsDeleted != isDeleted)
            {
                if (category.IsDeleted == false)
                {
                    this.categoriesRepository.Delete(category);
                }
                else
                {
                    category.IsDeleted = false;
                }
            }

            await this.categoriesRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.categoriesRepository.All()
                .To<T>();
        }

        public IEnumerable<T> GetAllWithDeleted<T>()
        {
            return this.categoriesRepository.AllAsNoTrackingWithDeleted()
                .To<T>();
        }

        public T GetById<T>(int id)
        {
            var category = this.categoriesRepository.All()
                .Where(x => id == x.Id)
                .To<T>()
                .FirstOrDefault();

            return category;
        }

        public T ByIdWithDeleted<T>(int id)
        {
            var category = this.categoriesRepository.AllWithDeleted()
                .Where(x => id == x.Id)
                .To<T>()
                .FirstOrDefault();

            return category;
        }

        public IEnumerable<T> GetAllRootCategories<T>()
        {
            return this.categoriesRepository.All()
                .Where(x => x.ParentCategoryId == null)
                .To<T>();
        }

        public IEnumerable<KeyValuePair<string, string>> GetCategoriesAsKeyValuePairs(int categoryId = 0)
        {
            return this.categoriesRepository.AllAsNoTracking()
                .Where(x => x.Id != categoryId)
                .OrderBy(x => x.Products.Count())
                .Select(x => new
                {
                    x.Name,
                    x.Id,
                })
                .ToList()
                .Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }

        public async Task DeleteCategoryById(int id)
        {
            var category = this.categoriesRepository.All().FirstOrDefault(x => x.Id == id);

            if (category != null)
            {
                this.categoriesRepository.Delete(category);
            }

            await this.categoriesRepository.SaveChangesAsync();
        }

        public IEnumerable<Category> GetCategoriesForProduct(int productId)
        {
            return this.categoriesRepository.All()
                .Where(x => x.Products.Any(p => p.ProductId == productId))
                .ToList();
        }

        public string GetCategoryName(int id)
        {
            var category = this.GetCategoryById(id);
            return category.Name;
        }

        public IEnumerable<CategorySidebarViewModel> GetAllMainCategoriesInfo()
        {
            var model = new List<CategorySidebarViewModel>();
            var rootCategories = this.categoriesRepository.All()
                .Where(x => x.ParentCategoryId == null);

            foreach (var category in rootCategories)
            {
                var mainCategory = AutoMapperConfig.MapperInstance.Map<CategorySidebarViewModel>(category);
                mainCategory.SubCategories = this.categoriesRepository.All()
                    .Where(x => x.ParentCategoryId == category.Id)
                    .To<BaseCategoryViewModel>()
                    .ToList();

                model.Add(mainCategory);
            }

            return model;
        }

        private Category GetCategoryById(int id)
        {
            return this.categoriesRepository.AllAsNoTrackingWithDeleted()
                .FirstOrDefault(x => id == x.Id);
        }
    }
}
