namespace WebStore.Web.ViewModels.ShoppingCart
{
    using System.ComponentModel.DataAnnotations;

    using AutoMapper.Configuration.Annotations;
    using WebStore.Data.Models;
    using WebStore.Services.Mapping;
    using WebStore.Web.ViewModels.Contact;

    public class CheckoutFormModel : IMapTo<Order>
    {
        [Required]
        public ContactFormModel Contact { get; set; }

        [Required]
        [Display(Name = "Shipping Method")]
        public string ShippingMethod { get; set; }

        [Required]
        [Display(Name = "Payment Method")]
        public string PaymentMethod { get; set; }

        [Ignore]
        public ItemInCartListViewModel CartItems { get; set; }
    }
}
