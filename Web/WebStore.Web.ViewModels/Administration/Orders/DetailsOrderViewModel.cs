
namespace WebStore.Web.ViewModels.Administration.Orders
{
    using System.Collections.Generic;

    using AutoMapper;
    using WebStore.Data.Models;
    using WebStore.Services.Mapping;
    using WebStore.Web.ViewModels.Contact;

    public class DetailsOrderViewModel : IMapFrom<SellerOrder>
    {
        public string Id { get; set; }

        public string SellerId { get; set; }

        public string CreatedOn { get; set; }

        public string PaymentMethod { get; set; }

        public string ShippingMethod { get; set; }

        public decimal TotalPrice { get; set; }

        public ContactOrderViewModel Contact { get; set; }

        public IEnumerable<ItemInOrderDetailsViewModel> Items { get; set; }

    }
}
