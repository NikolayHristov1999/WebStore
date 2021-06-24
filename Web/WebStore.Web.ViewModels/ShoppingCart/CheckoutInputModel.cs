namespace WebStore.Web.ViewModels.ShoppingCart
{
    using AutoMapper.Configuration.Annotations;
    using System.ComponentModel.DataAnnotations;
    using WebStore.Data.Models;
    using WebStore.Services.Mapping;

    public class CheckoutInputModel : IMapTo<Order>
    {
        [Required]
        public ContactInputModel Contact { get; set; }

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
