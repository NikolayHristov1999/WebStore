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
    using WebStore.Web.ViewModels.Categories;
    using WebStore.Web.ViewModels.Product;

    public class CategoriesService : ICategoriesService
    {
        private readonly IDeletableEntityRepository<Category> categoriesRepository;

        public CategoriesService(IDeletableEntityRepository<Category> categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public async Task CreateAsync(CategoryInputModel input)
        {
            var category = new Category
            {
                Description = input.Description,
                Name = input.Name,
                ImageUrl = input.ImageUrl,
            };
            if (input.CategoryId != null)
            {
                category.ParentCategoryId = int.Parse(input.CategoryId);
            }

            await this.categoriesRepository.AddAsync(category);
            await this.categoriesRepository.SaveChangesAsync();

        }

        public async Task UpdateAsync(int id, EditCategoryInputModel inputModel)
        {
            var category = this.categoriesRepository.AllWithDeleted().FirstOrDefault(x => x.Id == id);
            category.Name = inputModel.Name;
            category.Description = inputModel.Description;
            category.ImageUrl = inputModel.ImageUrl;
            category.ModifiedOn = DateTime.UtcNow;

            if (category.IsDeleted == false && inputModel.IsDeleted)
            {
                category.DeletedOn = DateTime.UtcNow;
            }

            category.IsDeleted = inputModel.IsDeleted;

            var isValidated = int.TryParse(inputModel.CategoryId, out int categoryId);
            if (isValidated && this.GetCategoryName(categoryId) != null)
            {
                category.ParentCategoryId = categoryId;
            }
            else
            {
                category.ParentCategoryId = null;
            }

            await this.categoriesRepository.SaveChangesAsync();
        }

        public EditCategoryInputModel GetProductEditModelById(int id)
        {
            var category = this.GetCategoryById(id);

            if (category == null)
            {
                return null;
            }

            var model = new EditCategoryInputModel
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                ImageUrl = category.ImageUrl,
                IsDeleted = category.IsDeleted,
                Categories = this.GetCategoriesAsKeyValuePairs(id),
            };

            int? categoryId = category.ParentCategoryId;
            if (categoryId != null)
            {
                model.CategoryId = categoryId.ToString();
            }

            return model;
        }

        public IEnumerable<T> GetAll<T>()
        {
            return this.categoriesRepository.AllAsNoTracking().To<T>();
        }

        public IEnumerable<T> GetAllWithDeleted<T>()
        {
            return this.categoriesRepository.AllAsNoTrackingWithDeleted().To<T>();
        }

        public T GetById<T>(int id)
        {
            var category = this.categoriesRepository.AllAsNoTracking().Where(x => id == x.Id)
                .To<T>().FirstOrDefault();

            return category;
        }

        public IEnumerable<T> GetAllRootCategories<T>()
        {
            return this.categoriesRepository.AllAsNoTrackingWithDeleted()
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
