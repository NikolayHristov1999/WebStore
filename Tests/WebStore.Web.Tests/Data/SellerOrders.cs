namespace WebStore.Web.Tests.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using WebStore.Data.Models;

    public class SellerOrders
    {
        public static IEnumerable<SellerOrder> GetTenSellerOrdersWithDealerId(string dealerId)
        {
            return Enumerable.Range(0, 10).Select(i => new SellerOrder
            {
                SellerId = dealerId,
            });
        }
    }
}
