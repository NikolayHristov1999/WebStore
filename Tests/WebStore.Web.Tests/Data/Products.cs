namespace WebStore.Web.Tests.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using WebStore.Data.Models;

    public static class Products
    {
        public static IEnumerable<Product> FifteenProducts
            => Enumerable.Range(0, 15).Select(i => new Product());
    }
}
