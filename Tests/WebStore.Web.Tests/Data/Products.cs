namespace WebStore.Web.Tests.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using WebStore.Data.Models;

    public static class Products
    {
        public static IEnumerable<Product> FifteenProducts
            => Enumerable.Range(0, 15).Select(i => new Product
            {
                AvailableQuantity = 10,
            });

        public static IEnumerable<Product> GetTenProductsWithDealerId(string dealerId)
        {
            return Enumerable.Range(0, 10).Select(i => new Product
            {
                AvailableQuantity = 10,
                AddedByUserId = dealerId,
            });
        }
    }
}
