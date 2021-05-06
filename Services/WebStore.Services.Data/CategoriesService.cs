namespace WebStore.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using WebStore.Data.Common.Repositories;
    using WebStore.Data.Models;
    using WebStore.Web.ViewModels.Categories;

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

        public IEnumerable<KeyValuePair<string, string>> GetCategoriesAsKeyValuePairs()
        {
            return this.categoriesRepository.AllAsNoTracking()
                .OrderBy(x => x.Products.Count())
                .Select(x => new
                {
                    x.Name,
                    x.Id,
                })
                .ToList()
                .Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }

        public string GetCategoryName(int id)
        {
            var category = this.GetCategoryById(id);
            return category.Name;
        }

        private Category GetCategoryById(int id)
        {
            return this.categoriesRepository.AllAsNoTracking()
                .FirstOrDefault(x => id == x.Id);
        }
    }
}
