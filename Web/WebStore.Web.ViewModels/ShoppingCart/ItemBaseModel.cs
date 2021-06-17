using System;
using System.Collections.Generic;
using System.Text;

namespace WebStore.Web.ViewModels.ShoppingCart
{
    public abstract class ItemBaseModel
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}
