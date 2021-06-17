
namespace WebStore.Services.Data
{
    using System.Threading.Tasks;
    using WebStore.Data.Models;

    public interface IShoppingCartService
    {
        Task<T> AddToCartAsync<T>(int cartId, int productId, int quantity)
            where T : class;

        Task AddItemToCartAsync(int cartId, Item item);

        bool CheckQuantity(int productId, int quantity);

        Task<int> CreateCartAsync(string userId = null);


    }
}
