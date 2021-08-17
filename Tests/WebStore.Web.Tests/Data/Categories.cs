
namespace WebStore.Web.Tests.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using WebStore.Data.Models;

    public class Categories
    {
        public static IEnumerable<Category> SixCategories
            => Enumerable.Range(0, 6).Select(i => new Category());
    }
}
