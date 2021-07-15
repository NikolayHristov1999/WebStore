namespace WebStore.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;

    using WebStore.Data.Common.Models;

    public class SellerOrder : BaseDeletableModel<string>
    {
        public SellerOrder()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Items = new HashSet<Item>();
        }

        public decimal TotalPrice { get; set; }

        [Required]
        public string ShippingMethod { get; set; }

        [Required]
        public string PaymentMethod { get; set; }

        public int ContactId { get; set; }

        public Contact Contact { get; set; }

        public string BuyerId { get; set; }

        public ApplicationUser Buyer { get; set; }

        public string SellerId { get; set; }

        public ApplicationUser Seller { get; set; }

        public virtual ICollection<Item> Items { get; set; }
    }
}
