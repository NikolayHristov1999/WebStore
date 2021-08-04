namespace WebStore.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using WebStore.Data.Models;

    public interface IDealerService
    {
        IEnumerable<Item> GetAllPurchasedItemsForSeller(string userId);

        IEnumerable<SellerOrder> GetAllOrdersForSeller(string userId);

        SellerOrder GetOrderForSellerById(string orderId);

        IEnumerable<Item> GetAllItemsBySellerOrderId(string sellerOrderId);

        T GetDealerByUserId<T>(string userId);

        Task<bool> ChangeDealerStatusAsync(string userId, string statusInput);

        bool IsDealerApproved(string userId);

        IEnumerable<T> GetAllDealers<T>();

    }
}
