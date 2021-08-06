namespace WebStore.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using WebStore.Common;
    using WebStore.Data.Models;
    using WebStore.Web.ViewModels.Administration.Products;
    using WebStore.Web.ViewModels.Product;

    public interface IProductsService
    {
        Task CreateAsync(CreateProductFormModel inputModel, string userId);

        Task UpdateAsync(int id, EditProductViewModel inputModel);

        /// <summary>
        ///     Get all products that are not deleted.
        /// </summary>
        /// <typeparam name="T">The view model to be returned.</typeparam>
        /// <param name="searchString">If there is a search query</param>
        /// <returns>Enumerable of the model type.</returns>
        IEnumerable<T> All<T>(string searchString = "");

        IEnumerable<T> GetAllForSeller<T>(string userId);

        IEnumerable<T> AllWithDeleted<T>(string searchString = "");

        IEnumerable<T> AllForSinglePage<T>(
            int page = 1,
            int productsPerPage = GlobalConstants.ProductsPerPage,
            string searchString = "");

        IEnumerable<T> AllForCategoryPage<T>(
            int categoryId,
            int page = 1,
            int productsPerPage = GlobalConstants.ProductsPerPage);

        IEnumerable<T> AllProductsInCategory<T>(int categoryId);

        T ById<T>(int id);

        T ByIdWithDeleted<T>(int id);

        Product GetProductById(int id);

        Task DeleteProductById(int id);

        Task IncreaseViewsNumberAsync(int id);

        bool IsUserOwner(string userId, int productId);

        string GetProductDealerId(int productId);
    }
}
