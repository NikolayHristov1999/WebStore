namespace WebStore.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Text;
    using WebStore.Data.Common.Models;

    public class Order : BaseDeletableModel<int>
    {
        [Required]
        public string ShippingMethod { get; set; }

        [Required]
        public string PaymentMethod { get; set; }

        public int CartId { get; set; }

        public Cart Cart { get; set; }

        public int ContactId { get; set; }

        public Contact Contact { get; set; }
 
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}
