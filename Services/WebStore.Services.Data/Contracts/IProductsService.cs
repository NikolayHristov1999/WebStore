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
        Task CreateAsync(CreateProductViewModel inputModel, string userId);

        EditProductViewModel GetEditProductModelById(int id);

        Task UpdateAsync(int id, EditProductViewModel inputModel);

        IEnumerable<T> GetAll<T>(string searchString = "");

        IEnumerable<T> GetAllForSeller<T>(string userId);

        IEnumerable<T> GetAllWithDeleted<T>(string searchString = "");

        IEnumerable<T> GetAllForSinglePage<T>(
            int page = 1,
            int productsPerPage = GlobalConstants.ProductsPerPage,
            string searchString = "");

        IEnumerable<T> GetAllForCategoryPage<T>(
            int categoryId,
            int page = 1,
            int productsPerPage = GlobalConstants.ProductsPerPage);

        IEnumerable<T> GetAllProductsInCategory<T>(int categoryId);

        T GetById<T>(int id);

        Product GetProductById(int id);

        Task DeleteProductById(int id);

        Task IncreaseViewsNumberAsync(int id);
    }
}
