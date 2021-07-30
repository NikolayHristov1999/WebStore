
namespace WebStore.Web.ViewModels.Administration.Orders
{
    using System.Collections.Generic;

    using WebStore.Web.ViewModels.Contact;

    public class OrderViewModel
    {
        public string Id { get; set; }

        public string CreatedOn { get; set; }

        public string PaymentMethod { get; set; }

        public string ShippingMethod { get; set; }

        public string TotalPrice { get; set; }

        public ContactOrderViewModel ContactInfo { get; set; }

        public IEnumerable<ItemViewModel> Items { get; set; }
    }
}
