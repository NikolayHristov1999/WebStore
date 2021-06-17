using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebStore.Data.Models;
using WebStore.Web.ViewModels.Product;

namespace WebStore.Services.Data
{
    public interface IProductsService
    {
        Task CreateAsync(ProductInputModel inputModel, string userId);

        EditProductInputModel GetProductEditModelById(int id);

        Task UpdateAsync(int id, EditProductInputModel inputModel);

        IEnumerable<T> GetAll<T>();

        IEnumerable<T> GetAllWithDeleted<T>();

        T GetById<T>(int id);

        Product GetProductById(int id);

        Task DeleteProductById(int id);
    }
}
