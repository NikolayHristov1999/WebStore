
namespace WebStore.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using WebStore.Data.Models;
    using WebStore.Web.ViewModels.ShoppingCart;

    public interface IShoppingCartService
    {
        Task<T> AddToCartAsync<T>(string cartId, int productId, int quantity)
            where T : class;

        Task AddItemToCartAsync(string cartId, Item item);

        bool CheckQuantity(int productId, int quantity);

        Task<string> CreateCartAsync(string userId = null);

        IEnumerable<T> GetAllItemsForCartId<T>(string cartId);

        Task<bool> RemoveItemFromCartAsync(string itemId, string cartId);

        Task<bool> CreateOrderAsync(CheckoutInputModel model, string cartId, string userId);

        int GetCartItemsCount(string cartId);
    }
}
