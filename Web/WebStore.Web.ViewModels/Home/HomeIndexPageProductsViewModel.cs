namespace WebStore.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using WebStore.Web.ViewModels.Product;

    public class HomeIndexPageProductsViewModel
    {
        public IEnumerable<ProductInListViewModel> NewProducts { get; set; }

        public IEnumerable<ProductInListViewModel> TopProducts { get; set; }
    }
}
