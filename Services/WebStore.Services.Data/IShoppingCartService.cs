
namespace WebStore.Services.Data
{
    using System.Threading.Tasks;

    public interface IShoppingCartService
    {
        Task<string> AddToCartAsync(string cartId, int productId, int quantity);
        
        bool CheckQuantity(int productId, int quantity);


    }
}
