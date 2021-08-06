namespace WebStore.Web.ViewModels.Administration.Orders
{
    public class DetailsSellerOrderViewModel
    {
        public string Id { get; set; }

        public string CreatedOn { get; set; }

        public string PaymentMethod { get; set; }

        public string ShippingMethod { get; set; }

        public string TotalPrice { get; set; }

        //public ContactOrderViewModel ContactInfo { get; set; }

        //public IEnumerable<ItemViewModel> Items { get; set; }
    }
}
