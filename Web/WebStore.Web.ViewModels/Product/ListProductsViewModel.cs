namespace WebStore.Web.ViewModels.Product
{
    using System.Collections.Generic;

    public class ListProductsViewModel : PagingViewModel
    {
        public IEnumerable<ProductInListViewModel> Products { get; set; }
    }
}
