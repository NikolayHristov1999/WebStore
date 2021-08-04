namespace WebStore.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface ICategoriesProductsService
    {
        Task RemoveAllByProductId(int id);

        Task AddAsync(int productId, int categoryId);
    }
}
