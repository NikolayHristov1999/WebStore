namespace WebStore.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using WebStore.Data.Models;

    public interface ISalesmanService
    {
        Task AddUserToRole(UserManager<ApplicationUser> user, string role);

        IEnumerable<Item> GetAllPurchasedItemsForSeller(string userId);

        IEnumerable<SellerOrder> GetAllOrdersForSeller(string userId);

        SellerOrder GetOrderForSellerById(string orderId);

        IEnumerable<Item> GetAllItemsBySellerOrderId(string sellerOrderId);

    }
}
