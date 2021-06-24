
namespace WebStore.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using WebStore.Data.Models;
    using WebStore.Web.ViewModels.ShoppingCart;

    public interface IShoppingCartService
    {
        Task<T> AddToCartAsync<T>(int cartId, int productId, int quantity)
            where T : class;

        Task AddItemToCartAsync(int cartId, Item item);

        bool CheckQuantity(int productId, int quantity);

        Task<int> CreateCartAsync(string userId = null);

        IEnumerable<T> GetAllItemsForCartId<T>(int cartId);

        Task<bool> RemoveItemFromCartAsync(string itemId, int cartId);

        Task<bool> CreateOrderAsync(CheckoutInputModel model, int cartId, string userId);

        int GetCartItemsCount(int cartId);
    }
}
