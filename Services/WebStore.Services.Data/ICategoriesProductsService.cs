namespace WebStore.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface ICategoriesProductsService
    {
        Task RemoveAllByProductId(int id);

    }
}
