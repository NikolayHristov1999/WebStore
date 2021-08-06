namespace WebStore.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;
    using WebStore.Data.Models;

    public interface IDealerService
    {
        IEnumerable<Item> GetAllPurchasedItemsForSeller(string userId);

        IEnumerable<T> GetAllOrdersForSeller<T>(string userId);

        T SellerOrderById<T>(string orderId);

        IEnumerable<Item> GetAllItemsBySellerOrderId(string sellerOrderId);

        T GetDealerByUserId<T>(string userId);

        Task<bool> ChangeDealerStatusAsync(string userId, string statusInput);

        bool IsDealerApproved(string userId);

        IEnumerable<T> GetAllDealers<T>();

        IEnumerable<T> AllOrdersForSellerInLastDays<T>(string userId, int days);

        int CountOfSalesForADay(string userId, DateTime day);

        decimal SumOfSalesForADay(string userId, DateTime day);

        double GetAverageRatingFromUsers(string userId);
    }
}
