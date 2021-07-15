
using System.Collections.Generic;

namespace WebStore.Web.ViewModels.Administration.Orders
{
    public class OrderViewModel
    {
        public string Id { get; set; }

        public string CreatedOn { get; set; }

        public string PaymentMethod { get; set; }

        public string ShippingMethod { get; set; }

        public string TotalPrice { get; set; }

        public ContactViewModel ContactInfo { get; set; }

        public IEnumerable<ItemViewModel> Items { get; set; }
    }
}
