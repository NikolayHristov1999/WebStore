namespace WebStore.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using WebStore.Web.ViewModels.Categories;

    public interface ICategoriesService
    {
        public IEnumerable<KeyValuePair<string, string>> GetCategoriesAsKeyValuePairs();

        Task CreateAsync(CategoryInputModel input);

        string GetCategoryName(int id);
    }
}
