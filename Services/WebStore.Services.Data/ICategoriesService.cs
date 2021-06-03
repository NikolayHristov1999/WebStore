﻿namespace WebStore.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using WebStore.Web.ViewModels.Categories;

    public interface ICategoriesService
    {
        public IEnumerable<KeyValuePair<string, string>> GetCategoriesAsKeyValuePairs(int categoryId = 0);

        EditCategoryInputModel GetProductEditModelById(int id);

        Task UpdateAsync(int id, EditCategoryInputModel inputModel);

        Task CreateAsync(CategoryInputModel input);

        string GetCategoryName(int id);

        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetAllWithDeleted<T>();

        IEnumerable<T> GetAllRootCategories<T>();

        T GetById<T>(int id);

        Task DeleteCategoryById(int id);
    }
}
