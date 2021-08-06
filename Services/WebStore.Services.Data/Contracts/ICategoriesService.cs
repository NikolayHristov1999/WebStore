namespace WebStore.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using WebStore.Data.Models;
    using WebStore.Web.ViewModels.Administration.Categories;
    using WebStore.Web.ViewModels.Categories;

    public interface ICategoriesService
    {
        public IEnumerable<KeyValuePair<string, string>> GetCategoriesAsKeyValuePairs(int categoryId = 0);

        Task UpdateAsync(
            int id,
            string name,
            string description,
            string imageUrl,
            string parentCategoryId,
            bool isDeleted);

        Task<int> CreateAsync(CategoryFormModel input);

        string GetCategoryName(int id);

        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetAllWithDeleted<T>();

        IEnumerable<T> GetAllRootCategories<T>();

        T GetById<T>(int id);

        T ByIdWithDeleted<T>(int id);

        IEnumerable<CategorySidebarViewModel> GetAllMainCategoriesInfo();

        IEnumerable<Category> GetCategoriesForProduct(int productId);

        Task DeleteCategoryById(int id);
    }
}
