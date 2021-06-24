
namespace WebStore.Web.ViewModels.ShoppingCart
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ItemInCartListViewModel
    {
        public IEnumerable<ItemInCartViewModel> Items { get; set; }

        public decimal TotalPrice => this.Items.Sum(x => x.ItemTotalPrice);
    }
}
